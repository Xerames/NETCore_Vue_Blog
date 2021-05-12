using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class BlogTag:IEntity
    {
        public int BlogId{ get; set; }
        public Blog Blog { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
