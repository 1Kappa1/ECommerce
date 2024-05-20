using System.ComponentModel.DataAnnotations;

namespace capanna.alessandro._5H.prenota.ViewModels;
public class RegisterVM
{
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public string? Surname{get; set;}

    [Required]
    public string? Username {get;set;}

    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber {get;set;}

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords don't match.")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }

    [DataType(DataType.MultilineText)]
    public string? Address { get; set; }
}
