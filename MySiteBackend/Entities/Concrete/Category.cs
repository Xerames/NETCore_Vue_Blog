using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
