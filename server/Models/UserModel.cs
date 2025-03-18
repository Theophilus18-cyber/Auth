using System.ComponentModel.DataAnnotations;

namespace server.Models
{
   public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; } // User's name
    public string Surname { get; set; } // User's surname
    public string IdOrPassport { get; set; } // User's ID or passport number
    public string Contact { get; set; } // User's contact number
    public string Email { get; set; } // User's email
    public string Password { get; set; } // Plain text password (not recommended for production)
    public bool Terms { get; set; } // Must be true to agree with terms & conditions
}

}
