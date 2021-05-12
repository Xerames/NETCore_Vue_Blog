using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Log:IEntity
    {
        public int Id { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
