
using System;
using System.Collections.Generic;
using System.Linq;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IBlogDal : IEntityRepository<Blog>
    {
        IQueryable<Blog> GetBlogsWithCategory();
        List<Blog> GetBlogsByCategoryId(int id);
        Blog AddWithTag(CreateUpdateBlogModel model);
        Blog GetBlogWithCategoryTagsAndCommentsWithReplies(int id);
        BlogDetail GetBlogWithCategoryTagsAndCommentsWithReplies(string slug);
        Blog GetBlogWithCategoryAndTags(int id);
        void UpdateWithTag(CreateUpdateBlogModel model);
        IQueryable<Blog> GetBlogsByTagName(string tagname);
        IQueryable<Blog> GetBlogsByCategoryName(string categoryname);
        IQueryable<Blog> SearchBlogs(string searchquery);
    }
}
