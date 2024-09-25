using FOPFilmHub.Data;
using FOPFilmHub.Library;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FOPFilmHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // Get user by ID
        [HttpGet("id/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userInfo = new UserInfo
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return Ok(userInfo);
        }

        // Get user by Username
        [HttpGet("username/{userName}")]
        public async Task<IActionResult> GetUserByUsername(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            var userInfo = new UserInfo
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
            return Ok(userInfo);
        }

        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.Select(user => new UserInfo
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email
            }).ToList();

            return Ok(users);
        }
    }
}
