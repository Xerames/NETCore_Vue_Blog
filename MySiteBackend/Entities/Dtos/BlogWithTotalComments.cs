using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class BlogWithTotalComments
    {
        public BlogDetail Blog { get; set; }
        public int TotalComments { get; set; }
    }
}
