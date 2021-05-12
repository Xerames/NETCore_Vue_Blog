using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Reply:IEntity
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public string Content { get; set; }
        public bool Confirmation { get; set; }
        public DateTime ReplyDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
