using DigimonApp.Domain.Models;
using DigimonApp.Utils.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace DigimonApp.Resources
{
    public class SaveDigimonResource
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Image is required")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Level is required")]
        [NumberRange(1, 6, ErrorMessage = "Please enter a value between 1 and 6")]
        public int? Level { get; set; }
    }
}
