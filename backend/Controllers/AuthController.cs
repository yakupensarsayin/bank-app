using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.DTO;
using Microsoft.IdentityModel.Tokens;
using backend.Authentication;
using backend.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using backend.Services.Concrete;
using System.Security.Cryptography;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IEmailerService _emailerService;

        public AuthController(IAuthService authService, IUserService userService, IRoleService roleService, IEmailerService emailerService)
        {
            _authService = authService;
            _userService = userService;
            _roleService = roleService;
            _emailerService = emailerService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (await _userService.DoesUserExist(dto.Email))
                return BadRequest(new AuthenticationResponse { Status=400, Message="Email is in use!"});

            string passwordHash = _authService.HashPassword(dto.Password);

            Role customerRole = await _roleService.GetRole(r => r.Name == "Customer");

            string emailToken = _authService.GenerateEmailVerificationToken();
            await _userService.RegisterUserToDatabase(dto, passwordHash, customerRole, emailToken);
            await _emailerService.SendEmailVerificationToken(dto.Email, emailToken);

            return Ok(new AuthenticationResponse { Status = 200, Message = "Successfully registered!!" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            User? user = await _userService.GetUserWithRoles(dto.Email);

            if (user == null || !_authService.VerifyPasswordHash(dto.Password, user.Password))
                return BadRequest(new AuthenticationResponse { Status= 400, Message = "Email or password is incorrect!"});

            if(!user.IsEmailConfirmed)
                return BadRequest(new AuthenticationResponse { Status = 400, Message = "Email is not verified!" });

            var response = _authService.CreateTokenResponse(user);
            int expiration = _authService.GetRefreshTokenExpirationMinutes();

            await _userService.SaveRefreshTokenToDatabase(user, response.RefreshToken, expiration);
            _authService.SetTokensInsideCookie(response, HttpContext);

            return Ok(new AuthenticationResponse { Status = 200, Message = "Succesfully logged in!"});
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh()
        {
            bool success = _authService.ExtractTokensFromCookie(HttpContext, out RefreshModel model);

            if(!success)
                return Unauthorized();

            TokenValidationResult result = await _authService.ValidateExpiredToken(model.AccessToken);

            if (!result.IsValid)
                return Unauthorized();

            string email = _authService.ExtractEmailClaim(result);

            User? user = await _userService.GetUserWithRoles(email);

            if (user == null || user.RefreshToken != model.RefreshToken || user.TokenExpiry < DateTime.UtcNow)
                return Unauthorized();

            var response = _authService.CreateTokenResponse(user);
            int expiration = _authService.GetRefreshTokenExpirationMinutes();

            await _userService.SaveRefreshTokenToDatabase(user, response.RefreshToken, expiration);

            _authService.SetTokensInsideCookie(response, HttpContext);

            return Ok();
        }

        [HttpGet("Verify")]
        public async Task<IActionResult> Verify([FromQuery] string token)
        {
            User? user = await _userService.GetUserByEmailVerificationToken(token);

            if (user == null)
                return BadRequest();

            await _userService.ConfirmUserEmail(user);

            return Ok();
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            string email = _authService.ExtractEmailClaim(HttpContext);

            await _userService.DeleteRefreshTokenFromDatabase(email);

            _authService.DeleteAuthenticationCookie(HttpContext);

            return Ok();
        }

    }
}
