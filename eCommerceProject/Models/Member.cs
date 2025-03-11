using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eCommerceProject.Models;

public class Member
{
    [Key]
    public int MemberId { get; set; }
    /// <summary>
    /// Email address of the member is set to null 
    /// with an exclamation mark to indicate that it will be updated later.
    ///  ! = null-forgiving operator
    /// </summary>
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? UserName { get; set; }
}

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [Compare(nameof(Email))]
    [Display(Name = "Confirm Email")]
    public string ConfirmEmail { get; set; }

    [Required]
    [StringLength(75, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password))]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }
    [Required]
    [StringLength(75, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}