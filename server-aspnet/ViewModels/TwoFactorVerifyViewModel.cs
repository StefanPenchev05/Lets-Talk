using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.ViewModels
{
    public class TwoFactorVerifyViewModel
    {
        [Required(ErrorMessage = "The Two Factor Authentication code is required")]
        public string TwoFactorAuthCode { get; set; }
    }
}