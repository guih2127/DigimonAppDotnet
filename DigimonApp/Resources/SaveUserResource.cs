using System.ComponentModel.DataAnnotations;

namespace DigimonApp.Resources
{
    public class SaveUserResource
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "The passwords doesnt match")]
        public string ConfirmPassword { get; set; }
    }
}
