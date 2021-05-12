using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Dtos;
using Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Core.Utilities.Responses.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("getcomments")]
        public IActionResult GetComments()
        {
            var comments = _commentService.GetList();
            return Ok(comments);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("getcomment/{id}")]
        public IActionResult GetComment(int id)
        {
            var comment = _commentService.Get(id);
            return Ok(comment);
        }

        [Authorize]
        [HttpPost("addcomment")]
        public IResponse AddComment(CommentViewModel model)
        {
            var result = _commentService.Add(model);
            return result;
        }

        [Authorize]
        [HttpPut("updatecomment")]
        public IResponse UpdateComment(CommentViewModel model)
        {
            var result = _commentService.Update(model);
            return result;
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("updatecommentbyadmin")]
        public IResponse UpdateCommentByAdmin(UpdateCommentByAdmin model)
        {
            var result = _commentService.UpdateCommentByAdmin(model);
            return result;
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("deletecomment/{id}")]
        public IResponse DeleteComment(int id)
        {
            var result = _commentService.Delete(id);
            return result;
        }
    }
}