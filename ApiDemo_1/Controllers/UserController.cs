using ApiDemo_1.Model;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiDemo_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public static readonly ILog Log = LogManager.GetLogger(typeof(UserController));

        [HttpGet("Admin")]
        [Authorize]
        public IActionResult AdminMethod()
        {
            UserModel model = getCurrentUser();
            return Ok($"Hello {model.UserName} Currenty you are in {model.Role} Role");
        }

        [HttpGet("Seller")]
        [Authorize]
        public IActionResult SellerMethod()
        {
            UserModel model = getCurrentUser();
            return Ok($"Hello {model.UserName} Currenty you are in {model.Role} Role");
        }


        [HttpGet("Public")]
        public string Public()
        {
            return "This is the Public method";
        }

        [NonAction]
        public UserModel getCurrentUser()
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            if (user != null)
            {
                var userClaims = user.Claims;
                return new UserModel
                {
                    UserName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                    GaveNumber = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                    Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
                    SurName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value,
                };
            }
            return null;
        }
    }
}
