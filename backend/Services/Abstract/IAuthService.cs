using backend.Authentication;
using backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services.Abstract
{
    public interface IAuthService
    {
        public TokenResponse CreateTokenResponse(User user);
        public void DeleteAuthenticationCookie(HttpContext context);
        public bool ExtractTokensFromCookie(HttpContext context, out RefreshModel model);
        public string ExtractEmailClaim(HttpContext context);
        public string ExtractEmailClaim(TokenValidationResult result);
        public int GetRefreshTokenExpirationMinutes();
        public string HashPassword(string password);
        public void SetTokensInsideCookie(TokenResponse response, HttpContext context);
        public Task<TokenValidationResult> ValidateExpiredToken(string jwtToken);
        public bool VerifyPasswordHash(string password, string hash);

        public string GenerateEmailVerificationToken();
    }
}
