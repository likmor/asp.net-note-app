using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using My_Cards.Contracts;
using My_Cards.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace My_Cards.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto request);
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
    }
    public class AuthService : IAuthService
    {
        private readonly UserManager<NotesUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<NotesUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<bool> RegisterAsync(RegisterDto req)
        {
            //var exists = await _userManager.FindByNameAsync(req.UserName);
            //if (exists != null)
            //{
            //    return false;
            //}
            var user = new NotesUser
            {
                UserName = req.UserName
            };
            var createUserResult = await _userManager.CreateAsync(user, req.Password);
            if (!createUserResult.Succeeded)
            {
                var errorString = "User Creation failed because: ";
                foreach (var error in createUserResult.Errors)
                {
                    errorString += " # " + error.Description;
                }
                await Console.Out.WriteLineAsync(errorString);
            }

            if (createUserResult.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }
                await _userManager.AddToRoleAsync(user, "User");
                await Console.Out.WriteLineAsync($"Created user {user.UserName}");
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto req)
        {
            var user = await _userManager.FindByNameAsync(req.UserName);
            if (user == null)
            {
                return null;
            }
            var loginResult = await _userManager.CheckPasswordAsync(user, req.Password);
            if (!loginResult)
            {
                return null;
            }
  
            var token = await GenerateJWTTokenAsync(user);
            return new LoginResponseDto
            {
                Token = token
            };
        }

        private async Task<string> GenerateJWTTokenAsync(NotesUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256);

            var tokenObject = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: signingCredentials
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return token;
        }
    }
}
