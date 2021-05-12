using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Confirmation { get; set; }
        public DateTime CommentDate { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Reply> Replies { get; set; }
    }
}
