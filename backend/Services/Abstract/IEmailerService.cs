namespace backend.Services.Abstract
{
    public interface IEmailerService
    {
        public Task SendEmailVerificationToken(string recipient, string emailToken);
    }
}
