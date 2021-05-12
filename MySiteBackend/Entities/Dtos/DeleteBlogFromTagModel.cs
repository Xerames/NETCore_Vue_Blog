using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class DeleteBlogFromTagModel
    {
        public int blogid { get; set; }
        public int tagid { get; set; }
    }
}
