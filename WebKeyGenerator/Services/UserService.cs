using Microsoft.EntityFrameworkCore;
using WebKeyGenerator.Context;
using WebKeyGenerator.Models.Buisness;
using WebKeyGenerator.Models.Identity;
using WebKeyGenerator.Models.Requests;
using WebKeyGenerator.Models.Responses;
using WebKeyGenerator.Utils;

namespace WebKeyGenerator.Services
{
    public class UserService : IUserService
    {
        private WebKeyGenerator.Utils.Logger logger = new Logger();
        private readonly AppDbContext db;
        private readonly ITokenService tokenService;
        public UserService(AppDbContext db, ITokenService tokenService)
        {
            this.db = db;
            this.tokenService = tokenService;
        }
        public DataResponse<TokenResponseData> Login(LoginRequest loginRequest)
        {
            logger.Trace($"Login {loginRequest.ToJson()}");
            var user = db.Users.SingleOrDefault(user => user.Active && user.Login == loginRequest.Login);
            if (user == null)
            {
                logger.Error("Login not found");
                return new DataResponse<TokenResponseData>() { IsOk = false, Message = "Login not found" };
            }
            var passwordHash = PasswordHelper.HashUsingPbkdf2(loginRequest.Password, Convert.FromBase64String(user.PasswordSalt));
            if (user.Password != passwordHash)
            {
                logger.Error("Invalid Password");
                return new DataResponse<TokenResponseData>() { IsOk = false, Message = "Invalid Password" };
            }
            var token = tokenService.GenerateTokens(user.Id, user.Role);
            return new DataResponse<TokenResponseData>()
            {
                IsOk = true,
                Data = new TokenResponseData()
                {
                    AccessToken = token.Item1,
                    RefreshToken = token.Item2,
                    Login=user.Login,
                    UserId=user.Id.ToString(),
                    UserRole=user.Role
                }
            };

        }
        public Response Logout(int userId)
        {
            logger.Trace($"LogutUserId: {userId}");

            var refreshToken = db.RefreshTokens.FirstOrDefault(o => o.UserId == userId);
            if (refreshToken == null)
            {
                return Response.OK;
            }
            db.RefreshTokens.Remove(refreshToken);
            var saveResponse = db.SaveChanges();
            if (saveResponse >= 0)
            {
                return Response.OK;
            }
            return Response.BadResponse("Unable to logout user");
        }
        public Response Signup(CreateUserRequest req)
        {
            logger.Trace($"SignUp: {req.ToJson()}");
            var existingUser = db.Users.SingleOrDefault(user => user.Login == req.Login);
            if (existingUser != null)
            {
                logger.Error("Логин занят!");

                return Response.BadResponse("Логин занят!");
            }


            var salt = PasswordHelper.GetSecureSalt();
            var passwordHash = PasswordHelper.HashUsingPbkdf2(req.Password, salt);
            var user = new User
            {
                Email = req.Email,
                Password = passwordHash,
                Login = req.Login,
                PasswordSalt = Convert.ToBase64String(salt),
                Active = true,
                Role = req.Role
            };
            db.Users.Add(user);
            var saveResponse = db.SaveChanges();
            if (saveResponse >= 0)
            {
                return Response.OK;
            }
            logger.Error("Ошибка добавления пользователя");

            return Response.BadResponse("Ошибка добавления пользователя");
        }
    }
}
