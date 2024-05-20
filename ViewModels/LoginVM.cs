using System.ComponentModel.DataAnnotations;

namespace capanna.alessandro._5H.prenota.ViewModels;

public class LoginVM
{
    [Required(ErrorMessage ="Username is required.")]
    public string? Username { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email {get;set;}

    [Required(ErrorMessage ="Password is required.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name ="Remember Me")]
    public bool RememberMe { get; set; }
}