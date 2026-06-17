using Identity.API.DTO;
using Identity.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        // Identity with SPA: cookie-based authentication and token-based authentication for APIs
        // https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-10.0

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            _logger.LogInformation("User {@Email} registration attempted", registerDto.Email);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError(error.Description);
                }

                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
            {
                return Unauthorized();
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!passwordValid)
            {
                return Unauthorized();
            }

            var token = GenerateToken(user);

            var newUserDto = new UserDto(token, user.FirstName, user.LastName, user.Email);

            _logger.LogInformation("User {@Email} logged in successfully", loginDto.Email);

            return Ok(newUserDto);
        }

        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo(CancellationToken cancellationToken)
        {
            // Log all claims to debug what arrives
            _logger.LogInformation("User authenticated: {IsAuthenticated}", User?.Identity?.IsAuthenticated ?? false);
            _logger.LogInformation("Incoming claims: {Claims}", User?.Claims?.Select(c => $"{c.Type}:{c.Value}").ToArray());

            //var email = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (User.Identity is not null && !User.Identity.IsAuthenticated && string.IsNullOrWhiteSpace(email))
                return NoContent();

            var user = await _userManager.FindByEmailAsync(email!);
            //var user = await _userManager.UserManager.GetUserByEmailWithAddress(User);

            if (user is null)
                return Unauthorized();

            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                //Address = user.Address?.ToDto(),
                //Roles = User.FindFirstValue(ClaimTypes.Role)
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout(CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

        private string GenerateToken(ApplicationUser user)
        {
            var tokenKey = _configuration["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(tokenKey))
            {
                throw new InvalidOperationException("Key is not configured.");
            }

            //if (tokenKey.Length < 64)
            //{
            //    throw new InvalidOperationException("Key must be at least 64 characters long for security reasons.");
            //}

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),                                // subject = user id (recommended)
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),           // standard jwt "email"
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: creds
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            _logger.LogInformation("Generated JWT token for user {Email}", user.Email);

            return token;
        }
    }
}