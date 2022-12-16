namespace DigimonApp.Domain.Models
{
    public class Digimon
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public DigimonLevelEnum Level { get; set; }
    }
}
