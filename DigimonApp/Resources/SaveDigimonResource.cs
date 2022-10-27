using System.ComponentModel.DataAnnotations;

namespace DigimonApp.Resources
{
    public class SaveDigimonResource
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [MaxLength(30)]
        public string Level { get; set; }
    }
}
