using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("gettags")]
        public IActionResult GetTags()
        {
            var tags = _tagService.GetList();
            return Ok(tags);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("gettag/{id}")]
        public IActionResult GetTag(int id)
        {
            var tag = _tagService.Get(id);
            return Ok(tag);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("gettagwithblogs/{id}")]
        public IActionResult GetTagWithBlogs(int id)
        {
            var tag = _tagService.GetTagWithBlogs(id);
            return Ok(tag);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addtag")]
        public IResponse AddTag(TagCreateUpdateModel model)
        {
            var result = _tagService.Add(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updatetag")]
        public IResponse UpdateTag(TagCreateUpdateModel model)
        {
            var result = _tagService.Update(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deletetag/{id}")]
        public IResponse DeleteTag(int id)
        {
            var result = _tagService.Delete(id);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("deleteblogfromtag")]
        public IResponse DeleteBlogFromTag(DeleteBlogFromTagModel model)
        {
            var result = _tagService.DeleteBlogFromTag(model.blogid,model.tagid);
            return result;
        }
    }

}
