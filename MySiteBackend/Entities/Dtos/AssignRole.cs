using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class AssignRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Exist { get; set; }
    }
}
