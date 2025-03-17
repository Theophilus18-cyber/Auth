using System.ComponentModel.DataAnnotations;

namespace server.Models
{
   public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string IdOrPassport { get; set; }
    public string Contact { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } // Plain text password (not recommended for production)
    public bool Terms { get; set; }
}

}
