using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.DTO;
using Microsoft.IdentityModel.Tokens;
using backend.Authentication;
using backend.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using backend.Services.Concrete;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AuthController(IAuthService authService, IUserService userService, IRoleService roleService)
        {
            _authService = authService;
            _userService = userService;
            _roleService = roleService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (await _userService.DoesUserExist(dto.Email))
                return BadRequest();

            string passwordHash = _authService.HashPassword(dto.Password);

            Role customerRole = await _roleService.GetRole(r => r.Name == "Customer");

            await _userService.RegisterUserToDatabase(dto, passwordHash, customerRole);

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            User? user = await _userService.GetUserWithRoles(dto.Email);

            if (user == null || !_authService.VerifyPasswordHash(dto.Password, user.Password))
                return BadRequest();

            var response = _authService.CreateTokenResponse(user);
            int expiration = _authService.GetRefreshTokenExpirationMinutes();

            await _userService.SaveRefreshTokenToDatabase(user, response.RefreshToken, expiration);
            _authService.SetTokensInsideCookie(response, HttpContext);

            return Ok();
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

        [HttpPost("Logout")]
        [Authorize]
        public async Task Logout()
        {
            string email = _authService.ExtractEmailClaim(HttpContext);

            await _userService.DeleteRefreshTokenFromDatabase(email);

            _authService.DeleteAuthenticationCookie(HttpContext);

            Ok();
        }

    }
}
