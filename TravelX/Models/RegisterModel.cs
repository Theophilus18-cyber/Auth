using System.ComponentModel.DataAnnotations;

public class RegisterModel
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string PassportID { get; set; } = string.Empty;

    [Required]
    public string Contact { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty; // ✅ Plaintext password
}
