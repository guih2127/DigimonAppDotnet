using System.ComponentModel.DataAnnotations;

namespace DigimonApp.Utils.CustomValidators
{
    // Ja existe uma DataAnnotation pra isso, mas queria testar essa funcionalidade de implementar um custom validator.
    public class NumberRange : ValidationAttribute
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public NumberRange(int _minimum, int _maximum)
        {
            Minimum = _minimum;
            Maximum = _maximum;
        }

        public override bool IsValid(object? value)
        {
            int? valueInt = (int?)value;
            if (valueInt < Minimum || valueInt > Maximum)
                return false;

            return true;
        }
    }
}
