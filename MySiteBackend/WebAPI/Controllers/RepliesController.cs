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
    public class RepliesController : ControllerBase
    {
        private IReplyService _replyService;
        public RepliesController(IReplyService replyService)
        {
            _replyService = replyService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getreplies")]
        public IActionResult GetReplies()
        {
            var replies = _replyService.GetList();
            return Ok(replies);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("getreply/{id}")]
        public IActionResult GetReply(int id)
        {
            var comment = _replyService.Get(id);
            return Ok(comment);
        }

        [Authorize]
        [HttpPost("addreply")]
        public IResponse AddReply(ReplyViewModel model)
        {
            var result = _replyService.Add(model);
            return result;
        }

        [Authorize]
        [HttpPut("updatereply")]
        public IResponse UpdateReply(ReplyViewModel model)
        {
            var result = _replyService.Update(model);
            return result;
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("updatereplybyadmin")]
        public IResponse UpdateReplyByAdmin(UpdateReplyByAdmin model)
        {
            var result = _replyService.UpdateReplyByAdmin(model);
            return result;
        }

        [Authorize(Roles="Admin")]
        [HttpDelete("deletereply/{id}")]
        public IResponse DeleteReply(int id)
        {
            var result = _replyService.Delete(id);
            return result;
        }
    }
}
