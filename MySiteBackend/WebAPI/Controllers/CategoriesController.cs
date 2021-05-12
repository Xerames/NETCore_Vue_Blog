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
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;
        private IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("getcategories")]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetList();
            return Ok(categories);
        }
        [HttpGet("getcategorieswithblogs")]
        public IActionResult GetCategoriesWithBlogs()
        {
            var categories = _categoryService.GetCategoriesWithBlogs();
            return Ok(categories);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getcategory/{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryService.Get(id);
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addcategory")]
        public IResponse AddCategory(CategoryViewModel model)
        {
            var result = _categoryService.Add(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updatecategory")]
        public IResponse UpdateCategory(CategoryViewModel model)
        {
            var result = _categoryService.Update(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deletecategory/{id}")]
        public IResponse DeleteCategory(int id)
        {
            var result = _categoryService.Delete(id);
            return result;
        }
    }
}