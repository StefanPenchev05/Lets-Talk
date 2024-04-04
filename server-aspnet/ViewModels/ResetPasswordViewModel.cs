using System.ComponentModel.DataAnnotations;
using Server.ValidatorValidationAttribute;

namespace Server.ViewModels
{
    public class ResetPassword
    {
        public string confirmPassword { get; set; }
    }
}