using System.ComponentModel.DataAnnotations;

namespace server.Models
{
 public class RegisterModel
{
    public string Name { get; set; }

    
    public string Surname { get; set; }

    public string IdOrPassport { get; set; }


    public string Contact { get; set; }


    [EmailAddress]
    public string Email { get; set; }

    [MinLength(8)]
    public string Password { get; set; }

    
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } // Used for validation only

    public bool Terms { get; set; }
}

}
