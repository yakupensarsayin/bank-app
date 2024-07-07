namespace backend.Authentication
{
    // TODO: When frontend is ready, add Refresh tokens.
    public class LoginResponse
    {
        public required string TokenType { get; set; }
        public required string AccessToken { get; set; }
    }
}
