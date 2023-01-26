using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebKeyGenerator.Models.Buisness;
using WebKeyGenerator.Models.Requests;
using WebKeyGenerator.Models.Responses;
using WebKeyGenerator.Services;

namespace WebKeyGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBaseEx
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;
        private readonly IDataService dataService;
        public UserController(IUserService userService, ITokenService tokenService, IDataService dataService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
            this.dataService = dataService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            return SafeRun(_ =>
            {

                if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Login) || string.IsNullOrEmpty(loginRequest.Password))
                {
                    return WebKeyGenerator.Models.Responses.Response.BadResponse("Некорректный логин и/или пароль");
                }
                var loginResponse = userService.Login(loginRequest);
                if (!loginResponse.IsOk)
                {
                    return WebKeyGenerator.Models.Responses.Response.BadResponse(String.IsNullOrEmpty(loginResponse.Message) ? "Unsuccessfully" : loginResponse.Message);

                }
                return loginResponse;

            });
        }


        [HttpPost]
        [Route("refresh_token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {

            return SafeRun(_ =>
            {
                if (refreshTokenRequest == null || string.IsNullOrEmpty(refreshTokenRequest.RefreshToken) || refreshTokenRequest.UserId == 0)
                {
                    throw new CodeException("Отсутствуют данные от рефреш токене");
                }
                var validateRefreshTokenResponse = tokenService.ValidateRefreshToken(refreshTokenRequest);
                if (!validateRefreshTokenResponse.IsOk)
                {
                    throw new CodeException(validateRefreshTokenResponse.Message);
                }
                var tokenResponse = tokenService.GenerateTokens(validateRefreshTokenResponse.Data.UserId, validateRefreshTokenResponse.Data.Role);
                return new DataResponse<TokenResponseData>()
                {
                    Data = new TokenResponseData()
                    {
                        AccessToken = tokenResponse.Item1,
                        RefreshToken = tokenResponse.Item2,
                        UserRole = validateRefreshTokenResponse.Data.Role,
                        UserId = validateRefreshTokenResponse.Data.UserId.ToString(),
                        Login = validateRefreshTokenResponse.Data.Login
                    }
                };
            });
        }


        [HttpPost]
        [Route("createuser")]
        public async Task<IActionResult> CreateUser(CreateUserRequest req)
        {
            return SafeRun(_ =>
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
                    if (errors.Any())
                    {
                        return WebKeyGenerator.Models.Responses.Response.BadResponse(String.Join(",", errors));

                    }
                }

                var signupResponse = userService.Signup(req);
                if (!signupResponse.IsOk)
                {
                    return UnprocessableEntity(signupResponse);
                }
                return WebKeyGenerator.Models.Responses.Response.OK;
            });
        }

        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            logger.Trace($"User id {UserID}");

            return SafeRun(_ =>
            {
                var logout = userService.Logout(UserID??default(int));
                if (!logout.IsOk)
                {
                    return UnprocessableEntity(logout);
                }
                return WebKeyGenerator.Models.Responses.Response.OK;
            });
        }




    }
}
