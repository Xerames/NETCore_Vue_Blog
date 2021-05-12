using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Core.Utilities;
using Core.Utilities.Responses.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private IBlogService _blogService;
        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("getblogs")]
        public IActionResult GetBlogs()
        {
            var blogs = _blogService.GetList();
            return Ok(blogs);
        }
        [HttpGet("getblogswithcategory")]
        public IResponse GetBlogsWithCategory([FromQuery]BlogQueryModel model)
        {
            var blogs = _blogService.GetBlogsWithCategory(model);
            return blogs;
        }
        [HttpGet("getlast5blogs")]
        public IActionResult GetLast5Blogs()
        {
            var blogs = _blogService.GetList().OrderByDescending(x => x.Id).Take(5).ToList();
            return Ok(blogs);
        }

        [HttpGet("getblogsbytagname/{tagname}")]
        public IResponse GetBlogsByTagName(string tagname,[FromQuery]BlogQueryModel model)
        {
            var blogs = _blogService.GetBlogsByTagName(tagname,model);
            return blogs;
        }

        [HttpGet("getblogsbycategoryname/{categoryname}")]
        public IResponse GetBlogsByCategoryName(string categoryname,[FromQuery] BlogQueryModel model)
        {
            var blogs = _blogService.GetBlogsByCategoryName(categoryname,model);
            return blogs;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getblog/{id}")]
        public IActionResult GetBlog(int id)
        {
            var blog = _blogService.Get(id);
            return Ok(blog);
        }
        [HttpGet("getblogwithcategorytagsandcommentswithreplies/{slug}")]
        public IActionResult GetBlogWithCategoryTagsAndCommentsWithReplies(string slug)
        {
            var blog = _blogService.GetBlogWithCategoryTagsAndCommentsWithReplies(slug);
            return Ok(blog);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getblogwithcategoryandtags/{id}")]
        public IActionResult GetBlogWithCategoryAndTags(int id)
        {
            var blog = _blogService.GetBlogWithCategoryAndTags(id);
            return Ok(blog);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("addblog")]
        public IResponse AddBlog(CreateUpdateBlogModel model)
        {
            var result = _blogService.AddWithTag(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateblog")]
        public IResponse UpdateBlog(CreateUpdateBlogModel model)
        {
            var result = _blogService.UpdateWithTag(model);
            return result;
        }

        
        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteblog/{id}")]
        public IResponse DeleteBlog(int id)
        {
            var result = _blogService.Delete(id);
            return result;
        }

        [HttpGet("searchblogs")]
        public IResponse SearchBlogs([FromQuery(Name ="search")]string searchquery,[FromQuery]BlogQueryModel model)
         {
            var searchblogs = _blogService.SearchBlogs(searchquery, model);
            return searchblogs;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("uploadimage")]
        public IResponse UploadImage([FromForm] ImageUploadModel model)
        {
            var result = _blogService.UploadImage(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("deleteimage")]
        public IResponse DeleteImage(DeleteImageModel model)
        {
            var result = _blogService.DeleteImage(model);
            return result;
        }
    }
}