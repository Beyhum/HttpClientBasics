using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientBasics.Models
{
    public class RegisterDto
    {
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }


        public RegisterDto(string emailToRegister, string pwordToRegister, string confirmedpwordToRegister)
        {
            email = emailToRegister;
            password = pwordToRegister;

            confirmPassword = confirmedpwordToRegister;

        }
    }
}
