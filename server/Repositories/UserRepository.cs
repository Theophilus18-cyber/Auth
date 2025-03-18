using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    // Connects to the database
    public UserRepository(DataContext context)
    {
        _context = context;
    }

      // Register a new user
    public async Task<UserModel> Register(RegisterModel registerData)
    {
        var user = new UserModel
        {
            Name = registerData.Name,
            Surname = registerData.Surname,
            IdOrPassport = registerData.IdOrPassport,
            Contact = registerData.Contact,
            Email = registerData.Email,
            Password = registerData.Password  // Directly assigns the password from RegisterModel
        };

        // Saves user in database
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;  // Return the registered user
    }

     // Login user (checks email or ID & compares password)
    public async Task<UserModel?> Login(string emailOrId, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == emailOrId || u.IdOrPassport == emailOrId);

        if (user == null || user.Password != password)  // Checks if user exists and password matches
            return null;

        return user;
    }

    // Check if user exists by email or ID
    public async Task<bool> UserExist(string emailOrId)
    {
        return await _context.Users.AnyAsync(u => u.Email == emailOrId || u.IdOrPassport == emailOrId);
    }

    // (Duplicate Register Method) Register UserModel directly
    public async Task<UserModel> Register(UserModel user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }
}