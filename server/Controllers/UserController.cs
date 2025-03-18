using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Repositories;
using System.Threading.Tasks;

namespace server.Controllers
{
    [Route("api/users")]
    
    [ApiController] // Makes this an API controller
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        // Connects this controller to the database
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

         //Registers a new user
        [HttpPost("register")] // This is a endpoint for /api/users/register 
        public async Task<IActionResult> Register([FromBody] RegisterModel registerData)
        {
            // Checks if the request data is correct
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Checks if the user already exists
            if (await _userRepository.UserExist(registerData.Email))
            {
                return BadRequest(new { message = "User already exists." });
            }

            // Creates a new user
            var userModel = new UserModel
            {
                Name = registerData.Name,
                Surname = registerData.Surname,
                IdOrPassport = registerData.IdOrPassport,
                Contact = registerData.Contact,
                Email = registerData.Email,
                Password = registerData.Password,
                Terms = registerData.Terms 
            };

            // Saves the user to the database
            var user = await _userRepository.Register(userModel);

            return Ok(new { message = "User registered successfully." });
        }

        // Login User
        [HttpPost("login")] // This is a endpoint for /api/users/login
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
