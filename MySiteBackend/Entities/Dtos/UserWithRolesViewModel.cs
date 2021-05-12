using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserWithRolesViewModel
    {
        public UserWithPhotoViewModel User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
