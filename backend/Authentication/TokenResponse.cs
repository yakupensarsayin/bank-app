namespace backend.Authentication
{
    // TODO: When frontend is ready, add Refresh tokens.
    public class TokenResponse
    {
        public required string TokenType { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required int ExpiresIn { get; set; }
    }
}
