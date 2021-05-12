using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class PasswordChangeByAdminModel
    {
        public string Id { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
