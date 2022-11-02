using DigimonApp.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace DigimonApp.Resources
{
    public class SaveDigimonResource
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Level is required")]
        public DigimonLevelEnum Level { get; set; }
    }
}
