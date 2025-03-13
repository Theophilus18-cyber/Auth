using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (existingUser != null)
            return BadRequest("Email is already registered");

        var user = new ApplicationUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            PassportID = model.PassportID,
            Contact = model.Contact,
            Email = model.Email,
            Password = model.Password, // ✅ Store plaintext password
            IsVerified = false
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
        if (user == null)
            return Unauthorized("Invalid credentials");

        return Ok("Login successful");
    }
}
