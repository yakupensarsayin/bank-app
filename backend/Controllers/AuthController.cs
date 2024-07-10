using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.DTO;
using backend.Mapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using backend.Authentication;
using System.Security.Cryptography;
using NuGet.Common;
using backend.Services.Abstract;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authManager;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AuthController(IAuthService authService, IUserService userService, IRoleService roleService)
        {
            _authManager = authService;
            _userService = userService;
            _roleService = roleService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (await _userService.DoesUserExist(dto.Email))
                return BadRequest();

            string passwordHash = _authManager.HashPassword(dto.Password);

            Role customerRole = await _roleService.GetRole(r => r.Name == "Customer");

            await _userService.RegisterUserToDatabase(dto, passwordHash, customerRole);

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            User? user = await _userService.GetUserWithRoles(dto.Email);

            if (user == null || !_authManager.VerifyPasswordHash(dto.Password, user.Password))
                return BadRequest();

            var response = _authManager.CreateTokenResponse(user);

            await _userService.SaveRefreshTokenToDatabase(user, response.RefreshToken, response.ExpiresIn);

            return Ok(response);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshModel model)
        {
            TokenValidationResult result = await _authManager.ValidateExpiredToken(model.AccessToken);

            if (!result.IsValid)
                return Unauthorized();

            string email = _authManager.ExtractEmailClaim(result);

            User? user = await _userService.GetUserWithRoles(email);

            if (user == null || user.RefreshToken != model.RefreshToken || user.TokenExpiry < DateTime.UtcNow)
                return Unauthorized();

            var response = _authManager.CreateTokenResponse(user);

            await _userService.SaveRefreshTokenToDatabase(user, response.RefreshToken, response.ExpiresIn);

            return Ok(response);
        }

    }
}
