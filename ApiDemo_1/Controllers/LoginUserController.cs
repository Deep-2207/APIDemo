using ApiDemo_1.Constant;
using ApiDemo_1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiDemo_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUserController : ControllerBase
    {
        public IConfiguration _configuration;
        public LoginUserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authentication(userLogin);

            if(user != null)
            {
                var token = GendrateToken(user);
                return Ok(token);
            }

            return NotFound("User is not Found");
        }

        private UserModel Authentication(UserLogin userLogin)
        {
            UserModel userModel = UserConstant.users.FirstOrDefault(x => x.UserName.ToLower() == userLogin.UserName.ToLower()
                                    && x.Password.ToLower() == userLogin.Password.ToLower());

            if(userModel != null)
            {
                return userModel;
            }
            return null;
        }

        private string GendrateToken(UserModel userModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var clamis = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userModel.UserName),
                new Claim(ClaimTypes.Email, userModel.Email),
                new Claim(ClaimTypes.GivenName, userModel.GaveNumber),
                new Claim(ClaimTypes.Surname, userModel.SurName),
                new Claim(ClaimTypes.Role, userModel.Role)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Key"], _configuration["Jwt:Audience"],
                clamis, expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
