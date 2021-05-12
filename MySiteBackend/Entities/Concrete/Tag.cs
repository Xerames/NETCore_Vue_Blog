using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Tag:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public IList<BlogTag> BlogTags { get; set; }
    }
}
