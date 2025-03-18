using server.Models;
using System.Threading.Tasks;


namespace server.Repositories{
    public interface IUserRepository{
        Task<UserModel>Register(UserModel user); // Saves a new user in the database
        Task<UserModel?>Login(string emailOrId , string password); // Checks user credentials for login

        Task<bool> UserExist(string emailOrId);  // Verifies if the user already exists
    }

}