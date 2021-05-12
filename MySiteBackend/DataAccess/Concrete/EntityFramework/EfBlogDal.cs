using System;
using System.Linq;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Entities.Dtos;
using AutoMapper;
using Core.DataAccess.EntityFramework;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.CodeAnalysis;
using Core.Utilities;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBlogDal : EfEntityRepositoryBase<Blog, MySite2Context>, IBlogDal
    {
        private IMapper _mapper;
        public EfBlogDal(IMapper mapper, MySite2Context context) : base(context)
        {
            _mapper = mapper;
        }

        public Blog AddWithTag(CreateUpdateBlogModel model)
        {
            var blog = _mapper.Map<Blog>(model);
            blog.BlogTags = model.Tagsids.Select(tagid => new BlogTag()
            {
                BlogId = blog.Id,
                TagId = tagid
            }).ToList();
            blog.CreatedOn = DateTime.Now;
            blog.Slug = SlugHelper.Slugify(model.Title);
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return blog;
        }

        public void UpdateWithTag(CreateUpdateBlogModel model)
        {
            var blog = _context.Blogs
                .Include(i => i.BlogTags)
                .FirstOrDefault(i => i.Id == model.Id);

            if (blog.Image == model.Image)
            {
                _mapper.Map(model, blog);
                blog.BlogTags = model.Tagsids.Select(tagid => new BlogTag()
                {
                    BlogId = blog.Id,
                    TagId = tagid
                }).ToList();
                blog.ModifiedOn = DateTime.Now;
                blog.Slug = SlugHelper.Slugify(model.Title);
                _context.SaveChanges();
            }
            else
            {
                FileManager.DeleteFile(blog.Image);
                _mapper.Map(model, blog);
                blog.BlogTags = model.Tagsids.Select(tagid => new BlogTag()
                {
                    BlogId = blog.Id,
                    TagId = tagid
                }).ToList();
                blog.ModifiedOn = DateTime.Now;
                blog.Slug = SlugHelper.Slugify(model.Title);
                _context.SaveChanges();
            }
        }

        public IQueryable<Blog> GetBlogsByCategoryName(string categoryname)
        {
            var blogs = _context.Blogs.AsQueryable();
            if (!string.IsNullOrEmpty(categoryname))
            {
                blogs = blogs
                    .Include(x => x.Category).Where(x => x.Category.Slug.ToLower() == categoryname.ToLower());
            }
            return blogs;
        }
        public IQueryable<Blog> GetBlogsByTagName(string tagname)
        {
            var blogs = _context.Blogs.AsQueryable();
            if (!string.IsNullOrEmpty(tagname))
            {
                blogs = blogs
                    .Include(i => i.Category)
                    .Include(i => i.BlogTags)
                    .ThenInclude(i => i.Tag)
                    .Where(i => i.BlogTags.Any(a => a.Tag.Slug.ToLower() == tagname.ToLower()));
            }
            return blogs;
        }

        public IQueryable<Blog> GetBlogsWithCategory()
        {
            var queryableblog = _context.Blogs.Include(blog => blog.Category).AsQueryable();
            return queryableblog;
        }

        public Blog GetBlogWithCategoryTagsAndCommentsWithReplies(int id)
        {
            //var blog = _context.Blogs
            //           .Where(blog => blog.Id == id)
            //           .Include(blog => blog.Category)
            //           .Include(blog => blog.BlogTags)
            //           .ThenInclude(blogtag => blogtag.Tag)
            //           .Include(blog => blog.Comments.Where(x => x.Confirmation == true))
            //           .ThenInclude(comment => comment.User)
            //           .ThenInclude(comment => comment.Replies.Where(x => x.Confirmation == true))
            //           .ThenInclude(reply => reply.User)
            //           .FirstOrDefault();
            //return blog;
            var blog = _context.Blogs
                      .Where(blog => blog.Id == id)
                      .Include(blog => blog.Category)
                      .Include(blog => blog.BlogTags)
                      .ThenInclude(blogtag => blogtag.Tag)
                      .Include(blog => blog.Comments).ThenInclude(comment => comment.User)
                      .Include(blog => blog.Comments.Where(x => x.Confirmation == true))
                      .ThenInclude(comment => comment.Replies.Where(x => x.Confirmation == true))
                      .ThenInclude(reply => reply.User)
                      .FirstOrDefault();
            return blog;
        }

        public Blog GetBlogWithCategoryAndTags(int id)
        {
            return _context.Blogs
                           .Where(x => x.Id == id)
                           .Include(x => x.Category)
                           .Include(x => x.BlogTags)
                           .ThenInclude(x => x.Tag)
                           .FirstOrDefault();
        }

        public List<Blog> GetBlogsByCategoryId(int id)
        {
            return _context.Blogs.Include(x => x.Category).Where(x => x.Category.Id == id).ToList();
        }



        public IQueryable<Blog> SearchBlogs(string searchquery)
        {
            return _context.Blogs.Where(blog => blog.Title.ToLower().Contains(searchquery.ToLower()) || blog.Content.ToLower().Contains(searchquery.ToLower())).Include(blog => blog.Category).AsQueryable();
        }

        public BlogDetail GetBlogWithCategoryTagsAndCommentsWithReplies(string slug)
        {
            //var blog = _context.Blogs
            //           .Where(blog => blog.Slug == slug)
            //           .Include(blog => blog.Category)
            //           .Include(blog => blog.BlogTags)
            //           .ThenInclude(blogtag => blogtag.Tag)
            //           .Include(blog => blog.Comments.Where(x => x.Confirmation == true))
            //           .ThenInclude(comment => comment.User)
            //           .ThenInclude(comment => comment.Replies.Where(x => x.Confirmation == true))
            //           .ThenInclude(reply => reply.User)
            //           .FirstOrDefault();
            //return blog;

            //var blog = _context.Blogs
            //           .Where(blog => blog.Slug == slug)
            //           .Include(blog => blog.Category)
            //           .Include(blog => blog.BlogTags)
            //           .ThenInclude(blogtag => blogtag.Tag)
            //           .Include(blog => blog.Comments).ThenInclude(comment => comment.User)
            //           .Include(blog => blog.Comments.Where(x => x.Confirmation == true))
            //           .ThenInclude(comment => comment.Replies.Where(x => x.Confirmation == true))
            //           .ThenInclude(reply => reply.User)
            //           .FirstOrDefault();
            //return blog;

            var blog = _context.Blogs.Where(blog => blog.Slug == slug).Select(blog => new BlogDetail
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Image = blog.Image,
                Slug = blog.Slug,
                BlogTags=blog.BlogTags.Select(blogtag=> new BlogTagDetail
                {
                    TagId=blogtag.TagId,
                    BlogId=blogtag.BlogId,
                    Tag=blogtag.Tag
                }),
                CreatedOn = blog.CreatedOn,
                ModifiedOn = blog.ModifiedOn,
                CategoryId = blog.CategoryId,
                Category = blog.Category,
                Comments = blog.Comments.Where(comment => comment.Confirmation == true).Select(comment => new CommentDetail
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    BlogId = blog.Id,
                    CommentDate = comment.CommentDate,
                    Confirmation = comment.Confirmation,
                    UserId = comment.User.Id,
                    User = new UserWithPhotoViewModel
                    {
                        Id = comment.User.Id,
                        FirstName = comment.User.FirstName,
                        LastName = comment.User.LastName,
                        UserName = comment.User.UserName,
                        Photo = comment.User.Photo
                    },
                    Replies=comment.Replies.Where(reply=>reply.Confirmation==true).Select(reply=> new ReplyDetail
                    {
                        Id=reply.Id,
                        CommentId=reply.CommentId,
                        Content=reply.Content,
                        Confirmation=reply.Confirmation,
                        ReplyDate=reply.ReplyDate,
                        UserId=reply.UserId,
                        User= new UserWithPhotoViewModel
                        {
                            Id = reply.User.Id,
                            FirstName = reply.User.FirstName,
                            LastName = reply.User.LastName,
                            UserName = reply.User.UserName,
                            Photo = reply.User.Photo
                        }
                    })
                })
            }).FirstOrDefault();
            return blog;
           
        }
    }
}

