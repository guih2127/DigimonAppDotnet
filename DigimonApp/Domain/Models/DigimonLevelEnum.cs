using System.ComponentModel;

namespace DigimonApp.Domain.Models
{
    public enum DigimonLevelEnum : byte
    {
        [Description("Baby")]
        BABY = 1,

        [Description("In-Training")]
        IN_TRAINING = 2,

        [Description("Rookie")]
        ROOKIE = 3,

        [Description("Champion")]
        CHAMPION = 4,

        [Description("Ultimate")]
        ULTIMATE = 5,

        [Description("Mega")]
        MEGA = 6,
    }
}
