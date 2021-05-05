using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Pixie_Ticketing.Models
{
    public class LoginModel
    {
        [Required()]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required()]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}