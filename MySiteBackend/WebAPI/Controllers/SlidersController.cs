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
using Microsoft.AspNetCore.Authorization;
using Core.Utilities;
using Core.Utilities.Responses.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private ISliderService _sliderService;
        public SlidersController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpGet("getsliders")]
        public IActionResult GetSliders()
        {
            var sliders = _sliderService.GetList();
            return Ok(sliders);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getslider/{id}")]
        public IActionResult GetSlider(int id)
        {
            var slider = _sliderService.Get(id);
            return Ok(slider);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addslider")]
        public IResponse AddSlider(SliderViewModel model)
        {
            var result = _sliderService.Add(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateslider")]
        public IResponse UpdateSlider(SliderViewModel model)
        {
            var result = _sliderService.Update(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteslider/{id}")]
        public IResponse DeleteSlider(int id)
        {
            var result = _sliderService.Delete(id);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("uploadimage")]
        public IResponse UploadImage([FromForm] ImageUploadModel model)
        {
            var result = _sliderService.UploadImage(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("deleteimage")]
        public IResponse DeleteImage(DeleteImageModel model)
        {
            var result = _sliderService.DeleteImage(model);
            return result;
        }
    }
}