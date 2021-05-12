using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ResetPasswordModel
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
