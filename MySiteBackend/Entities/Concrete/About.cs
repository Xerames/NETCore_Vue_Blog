using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class About: IEntity
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
