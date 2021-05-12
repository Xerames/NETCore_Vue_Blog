using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UpdateCommentByAdmin
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public string UserId { get; set; }
        public bool Confirmation { get; set; }
    }
}
