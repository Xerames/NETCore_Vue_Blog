
using System;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface ITagDal : IEntityRepository<Tag>
    {
        Tag GetTagWithBlogs(int id);
        int DeleteBlogFromTag(int blogid, int tagid);
    }
}
