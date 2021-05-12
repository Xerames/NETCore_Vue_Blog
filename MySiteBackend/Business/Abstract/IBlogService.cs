
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IBlogService
    {
        Blog Get(int id);
        IResponse SearchBlogs(string searchquery, BlogQueryModel model);
        List<Blog> GetList();
        IResponse AddWithTag(CreateUpdateBlogModel model);
        IResponse UpdateWithTag(CreateUpdateBlogModel model);
        IResponse Delete(int id);
        IResponse GetBlogsWithCategory(BlogQueryModel model);
        List<Blog> GetBlogsByCategoryId(int id);
        //IResponse GetBlogWithCategoryTagsAndCommentsWithReplies(int id);
        IResponse GetBlogWithCategoryTagsAndCommentsWithReplies(string slug);
        Blog GetBlogWithCategoryAndTags(int id);
        IResponse GetBlogsByTagName(string tagname, BlogQueryModel model);
        IResponse GetBlogsByCategoryName(string categoryname, BlogQueryModel model);
        IResponse UploadImage(ImageUploadModel model);
        IResponse DeleteImage(DeleteImageModel model);

    }
}
