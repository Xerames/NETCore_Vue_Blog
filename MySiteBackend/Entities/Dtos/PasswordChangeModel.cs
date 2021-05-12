using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class PasswordChangeModel
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
