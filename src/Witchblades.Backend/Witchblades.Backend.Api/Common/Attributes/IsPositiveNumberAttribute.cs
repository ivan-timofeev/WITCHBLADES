#nullable disable
using System.ComponentModel.DataAnnotations;

namespace Witchblades.Backend.Api.Utils.Attributes
{
    public class IsPositiveNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string number = value.ToString();
            bool isPositiveNumber = !number.Contains('-');

            return isPositiveNumber;
        }
    }
}

