using System;
using System.Collections.Generic;
using System.IO;
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
    public class AboutsController : ControllerBase
    {
        private IAboutService _aboutService;
        public AboutsController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet("getabouts")]
        public IActionResult GetAbouts()
        {
            var abouts = _aboutService.GetList();
            return Ok(abouts);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getabout/{id}")]
        public IActionResult GetAbout(int id)
        {
            var about = _aboutService.Get(id);
            return Ok(about);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addabout")]
        public IResponse AddAbout(AboutViewModel model)
        {
            var result = _aboutService.Add(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateabout")]
        public IResponse UpdateAbout(AboutViewModel model)
        {
            var result = _aboutService.Update(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteabout/{id}")]
        public IResponse DeleteAbout(int id)
        {
            var result = _aboutService.Delete(id);
            return result;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("uploadimage")]
        public IResponse UploadImage([FromForm]ImageUploadModel model)
        {
            var result = _aboutService.UploadImage(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("deleteimage")]
        public IResponse DeleteImage(DeleteImageModel model)
        {
            var result = _aboutService.DeleteImage(model);
            return result;
        }
    }

}