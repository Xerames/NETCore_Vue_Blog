using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ReplyDetail
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; }
        public bool Confirmation { get; set; }
        public DateTime ReplyDate { get; set; }
        public string UserId { get; set; }
        public UserWithPhotoViewModel User { get; set; }
    }
}
