using server.Models;
using System.Threading.Tasks;


namespace server.Repositories{
    public interface IUserRepository{
        Task<UserModel>Register(UserModel user);
        Task<UserModel?>Login(string emailOrId , string password);

        Task<bool> UserExist(string emailOrId);
    }

}