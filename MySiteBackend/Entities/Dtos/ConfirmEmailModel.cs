using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ConfirmEmailModel
    {
        public string userId { get; set; }
        public string token { get; set; }
    }
}
