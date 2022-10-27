using DigimonApp.Domain.Models;

namespace DigimonApp.Domain.Services.Communication
{
    public class DigimonResponse : BaseResponse
    {
        public Digimon Digimon { get; private set; }

        private DigimonResponse(bool success, string message, Digimon digimon) : base(success, message)
        {
            Digimon = digimon;
        }

        public DigimonResponse(Digimon digimon) : this(true, string.Empty, digimon) { }

        public DigimonResponse(string message) : this(false, message, null) { }
    }
}
