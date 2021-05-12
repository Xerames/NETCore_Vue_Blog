using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class CommentDetail
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Confirmation { get; set; }
        public DateTime CommentDate { get; set; }
        public int BlogId { get; set; }
        public string UserId { get; set; }
        public UserWithPhotoViewModel User { get; set; }
        public IEnumerable<ReplyDetail> Replies { get; set; }
    }
}
