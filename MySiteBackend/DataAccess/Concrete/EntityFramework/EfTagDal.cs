
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfTagDal : EfEntityRepositoryBase<Tag, MySite2Context>, ITagDal
    {
        public EfTagDal(MySite2Context context) : base(context)
        {
            
        }
        public int DeleteBlogFromTag(int blogid, int tagid)
        {
            var cmd = @"delete from BlogTags where BlogId=@p0 And TagId=@p1";
            _context.Database.ExecuteSqlRaw(cmd, blogid, tagid);
            return tagid;
        }

        public Tag GetTagWithBlogs(int id)
        {
            return _context.Tags
                .Where(i => i.Id == id)
                .Include(i => i.BlogTags)
                .ThenInclude(i => i.Blog)
                .FirstOrDefault();
        }
    }
}
