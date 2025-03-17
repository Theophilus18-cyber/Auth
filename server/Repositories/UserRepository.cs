using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    // Register logic with direct password assignment
    public async Task<UserModel> Register(RegisterModel registerData)
    {
        var user = new UserModel
        {
            Name = registerData.Name,
            Surname = registerData.Surname,
            IdOrPassport = registerData.IdOrPassport,
            Contact = registerData.Contact,
            Email = registerData.Email,
            Password = registerData.Password  // Directly assign the password from RegisterModel
        };

        // Add the user to the database
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;  // Return the registered user
    }

    // Login logic (plain text password comparison)
    public async Task<UserModel?> Login(string emailOrId, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == emailOrId || u.IdOrPassport == emailOrId);

        if (user == null || user.Password != password)  // Direct comparison (plain text)
            return null;

        return user;
    }

    // Check if user exists by email or ID
    public async Task<bool> UserExist(string emailOrId)
    {
        return await _context.Users.AnyAsync(u => u.Email == emailOrId || u.IdOrPassport == emailOrId);
    }

    // Implement the Register method
    public async Task<UserModel> Register(UserModel user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }
}