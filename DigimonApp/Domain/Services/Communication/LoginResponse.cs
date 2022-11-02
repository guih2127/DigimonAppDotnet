using DigimonApp.Domain.Models;
using DigimonApp.Resources;

namespace DigimonApp.Domain.Services.Communication
{
    public class LoginResponse : BaseResponse
    {
        public JwtResource Token { get; private set; }

        private LoginResponse(bool success, string message, JwtResource token) : base(success, message)
        {
            Token = token;
        }

        public LoginResponse(JwtResource token) : this(true, string.Empty, token) { }

        public LoginResponse(string message) : this(false, message, null) { }
    }
}
