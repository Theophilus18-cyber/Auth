using System.ComponentModel.DataAnnotations;

namespace server.Models
{
 public class RegisterModel
{
    public string Name { get; set; } // User's name

    
    public string Surname { get; set; } // User's surname

    public string IdOrPassport { get; set; } // User's ID or passport number


    public string Contact { get; set; } // User's contact number


    [EmailAddress]
    public string Email { get; set; } // User's email

    [MinLength(8)]
    public string Password { get; set; } // User's password

    
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } // Used for validation only

    public bool Terms { get; set; } // Must be true to agree with terms & conditions
}

}
