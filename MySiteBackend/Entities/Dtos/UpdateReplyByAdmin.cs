using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UpdateReplyByAdmin
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public bool Confirmation { get; set; }
    }
}
