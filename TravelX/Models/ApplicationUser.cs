using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Unique ID for each user

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string PassportID { get; set; } = string.Empty;

    [Required]
    [MaxLength(15)]
    public string Contact { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty; // ✅ Stores plaintext password

    public bool IsVerified { get; set; } = false;
}
