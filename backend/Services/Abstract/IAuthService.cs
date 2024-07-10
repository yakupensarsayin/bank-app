using backend.Authentication;
using backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace backend.Services.Abstract
{
    public interface IAuthService
    {
        public TokenResponse CreateTokenResponse(User user);
        public string ExtractEmailClaim(TokenValidationResult result);
        public string HashPassword(string password);
        public Task<TokenValidationResult> ValidateExpiredToken(string jwtToken);
        public bool VerifyPasswordHash(string password, string hash);
    }
}
