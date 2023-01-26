using WebKeyGenerator.Models.Buisness;
using WebKeyGenerator.Models.Requests;
using WebKeyGenerator.Models.Responses;

namespace WebKeyGenerator.Services
{
    public interface IUserService
    {
        DataResponse<TokenResponseData> Login(LoginRequest loginRequest);
        Response Signup(CreateUserRequest signupRequest);
        Response Logout(int userId);
    }
}
