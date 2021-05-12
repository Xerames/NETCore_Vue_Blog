using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public string UserId { get; set; }
    }
}
