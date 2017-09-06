using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarCare.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
    }
}