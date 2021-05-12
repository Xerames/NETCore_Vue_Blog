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
    public class MinistrationsController : ControllerBase
    {
        private IMinistrationService _ministrationService;
        public MinistrationsController(IMinistrationService ministrationService)
        {
            _ministrationService = ministrationService;
        }

        [HttpGet("getministrations")]
        public IActionResult GetMinistrations()
        {
            var ministrations = _ministrationService.GetList();
            return Ok(ministrations);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("getministration/{id}")]
        public IActionResult GetMinistration(int id)
        {
            var ministration = _ministrationService.Get(id);
            return Ok(ministration);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addministration")]
        public IResponse AddMinistration(MinistrationViewModel model)
        {
            var result = _ministrationService.Add(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateministration")]
        public IResponse UpdateMinistration(MinistrationViewModel model)
        {
            var result = _ministrationService.Update(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteministration/{id}")]
        public IResponse DeleteMinistration(int id)
        {
            var result = _ministrationService.Delete(id);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("uploadimage")]
        public IResponse UploadImage([FromForm] ImageUploadModel model)
        {
            var result = _ministrationService.UploadImage(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("deleteimage")]
        public IResponse DeleteImage(DeleteImageModel model)
        {
            var result = _ministrationService.DeleteImage(model);
            return result;
        }
    }
}