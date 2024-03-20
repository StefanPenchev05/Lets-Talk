using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Server.Validator
{
    public class Username : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var username = value as string;

            if(!Regex.IsMatch(username,@"^[a-zA-Z0-9]+$"))
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return "Username can only contain alphanumeric characters";
        }
    }
}