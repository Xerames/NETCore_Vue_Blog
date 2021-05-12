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
    public class ContactsController : ControllerBase
    {
        private IContactService _contactService;
        private IMapper _mapper;
        public ContactsController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet("getcontacts")]
        public IActionResult GetContacts()
        {
            var contacts = _contactService.GetList();
            return Ok(contacts);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getcontact/{id}")]
        public IActionResult GetContact(int id)
        {
            var contact = _contactService.Get(id);
            return Ok(contact);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addcontact")]
        public IResponse AddContact(ContactViewModel model)
        {
            var result = _contactService.Add(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updatecontact")]
        public IResponse UpdateContact(ContactViewModel model)
        {
            var result = _contactService.Update(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deletecontact/{id}")]
        public IResponse DeleteContact( int id)
        {
            var result = _contactService.Delete(id);
            return result;
        }
    }
}