using WebKeyGenerator.Models.Buisness;
using WebKeyGenerator.Models.Identity;
using WebKeyGenerator.Models.Requests;
using WebKeyGenerator.Models.Responses;

namespace WebKeyGenerator.Services
{
    public interface ITokenService
    {
        Tuple<string, string> GenerateTokens(int userId, string role);
        DataResponse<ValidateRefreshTokenResponseData> ValidateRefreshToken(RefreshTokenRequest refreshTokenRequest);
        bool RemoveRefreshToken(User user);
    }
}
