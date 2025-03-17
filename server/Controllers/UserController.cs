using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Repositories;
using System.Threading.Tasks;

namespace server.Controllers
{
    [Route("api/users")]
    
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Register User
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the user already exists
            if (await _userRepository.UserExist(registerData.Email))
            {
                return BadRequest(new { message = "User already exists." });
            }

            // Register the user
            var userModel = new UserModel
            {
                Name = registerData.Name,
                Surname = registerData.Surname,
                IdOrPassport = registerData.IdOrPassport,
                Contact = registerData.Contact,
                Email = registerData.Email,
                Password = registerData.Password,
                Terms = registerData.Terms // Store the password as it is (ensure you handle it properly in your repository)
            };

            var user = await _userRepository.Register(userModel);

            return Ok(new { message = "User registered successfully." });
        }

        // Login User
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepository.Login(loginData.EmailOrId, loginData.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(new { message = "Login successful", user });
        }
    }
}
