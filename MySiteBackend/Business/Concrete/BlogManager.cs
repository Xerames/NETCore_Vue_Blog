
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using Business.Abstract;
using Core.Utilities;
using Entities.Dtos;
using AutoMapper;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using Core.Utilities.Extensions;
using Business.Constants;
using Core.Aspects.Autofac.Validation;
using Core.Exceptions;

namespace Business.Concrete
{
    public class BlogManager : IBlogService
    {
        private IBlogDal _blogDal;
        private IMapper _mapper;
        public BlogManager(IBlogDal blogDal, IMapper mapper)
        {
            _blogDal = blogDal;
            _mapper = mapper;
        }

        public Blog Get(int id)
        {
            return _blogDal.Get(x => x.Id == id);
        }

        public List<Blog> GetList()
        {
            return _blogDal.GetList();
        }

        public IResponse GetBlogsWithCategory(BlogQueryModel model)
        {
            var queryableblog = _blogDal.GetBlogsWithCategory();
            var pagedblog = queryableblog.ApplyPaging(model);
            return new PagedDataResponse<IQueryable<Blog>>(pagedblog, 200, queryableblog.Count());
        }

        public List<Blog> GetBlogsByCategoryId(int id)
        {
            return _blogDal.GetBlogsByCategoryId(id);
        }

        [ValidationAspect(typeof(AddBlogValidator))]
        public IResponse AddWithTag(CreateUpdateBlogModel model)
        {
            var blog = _blogDal.AddWithTag(model);
            return new DataResponse<Blog>(blog, 200,Messages.Added);
        }

        [ValidationAspect(typeof(UpdateBlogValidator))]
        public IResponse UpdateWithTag(CreateUpdateBlogModel model)
        {
            var existblog = _blogDal.Get(x => x.Id == model.Id);
            if (existblog == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            _blogDal.UpdateWithTag(model);
            return new SuccessResponse(200, Messages.Updated);
        }

     
        public IResponse Delete(int id)
        {
            var blog = _blogDal.Get(x => x.Id == id);
            if (blog == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            FileManager.DeleteFile(blog.Image);
            _blogDal.Delete(blog);
            return new SuccessResponse(200, Messages.Deleted);
        }

        public IResponse GetBlogsByTagName(string tagname, BlogQueryModel model)
        {
            var blogsbytagname = _blogDal.GetBlogsByTagName(tagname);
            var pagedblog = blogsbytagname.ApplyPaging(model);
            return new PagedDataResponse<IQueryable<Blog>>(pagedblog, 200, blogsbytagname.Count());
        }

        public IResponse GetBlogsByCategoryName(string categoryname, BlogQueryModel model)
        {
            var blogsbycategoryname = _blogDal.GetBlogsByCategoryName(categoryname);
            var pagedblog = blogsbycategoryname.ApplyPaging(model);
            return new PagedDataResponse<IQueryable<Blog>>(pagedblog, 200, blogsbycategoryname.Count());
        }

        public Blog GetBlogWithCategoryAndTags(int id)
        {
            return _blogDal.GetBlogWithCategoryAndTags(id);
        }

        public IResponse SearchBlogs(string searchquery, BlogQueryModel model)
        {
            var searchblogs = _blogDal.SearchBlogs(searchquery);
            var pagedsearchblogs = searchblogs.ApplyPaging(model);
            return new PagedDataResponse<IQueryable<Blog>>(pagedsearchblogs, 200, searchblogs.Count());
        }

        public IResponse GetBlogWithCategoryTagsAndCommentsWithReplies(string slug)
        {
            var blog = _blogDal.GetBlogWithCategoryTagsAndCommentsWithReplies(slug);
            int totalcomments = 0;

            var comments = blog.Comments.ToList();
            for (int i = 0; i < comments.Count; i++)
            {
                totalcomments++;
                for (int j = 0; j < comments[i].Replies.ToList()?.Count; j++)
                {
                    totalcomments++;
                }
            }
            //var totalcomments = comments.Select(i => (i.Replies.ToList()?.Count ?? 0) + 1).Sum();
            var blogwithtotalcomments = new BlogWithTotalComments
            {
                Blog = blog,
                TotalComments = totalcomments
            };
            return new DataResponse<BlogWithTotalComments>(blogwithtotalcomments, 200);
        }

        [ValidationAspect(typeof(UploadImageValidator))]
        public IResponse UploadImage(ImageUploadModel model)
        {
            var imagepath = FileManager.SaveFile(FolderNames.Blogs, model.Image);
            return new DataResponse<string>(imagepath, 200);
        }

        public IResponse DeleteImage(DeleteImageModel model)
        {
            FileManager.DeleteFile(model.Image);
            return new SuccessResponse(200, Messages.ImageDeleted);
        }
    }

}