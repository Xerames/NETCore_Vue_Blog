using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class BlogTagDetail
    {
        public int BlogId { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
