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
    public class SiteInformationsController : ControllerBase
    {
        private ISiteInformationService _siteinformationService;
        private IMapper _mapper;
        public SiteInformationsController(ISiteInformationService siteinformationService, IMapper mapper)
        {
            _siteinformationService = siteinformationService;
            _mapper = mapper;
        }

        [HttpGet("getsiteinformations")]
        public IActionResult GetSiteInformations()
        {
            var siteinformations = _siteinformationService.GetList();
            return Ok(siteinformations);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getsiteinformation/{id}")]
        public IActionResult GetSiteInformation(int id)
        {
            var siteinformation = _siteinformationService.Get(id);
            return Ok(siteinformation);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addsiteinformation")]
        public IResponse AddSiteInformation(SiteInformationViewModel model)
        {
            var result = _siteinformationService.Add(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updatesiteinformation")]
        public IResponse UpdateSiteInformation(SiteInformationViewModel model)
        {
            var result = _siteinformationService.Update(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deletesiteinformation/{id}")]
        public IResponse DeleteSiteInformation(int id)
        {
            var result = _siteinformationService.Delete(id);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("uploadimage")]
        public IResponse UploadImage([FromForm] ImageUploadModel model)
        {
            var result = _siteinformationService.UploadImage(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("deleteimage")]
        public IResponse DeleteImage(DeleteImageModel model)
        {
            var result = _siteinformationService.DeleteImage(model);
            return result;
        }
    }
}