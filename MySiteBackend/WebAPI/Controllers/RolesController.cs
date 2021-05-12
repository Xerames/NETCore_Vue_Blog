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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("getroles")]
        public IResponse GetRoles()
        {
            var roles = _roleService.ListRole();
            return roles;
        }

        [HttpGet("getrole/{id}")]
        public async Task<IResponse> GetRole(string id)
        {
            var result = await _roleService.GetRole(id);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addrole")]
        public async Task<IResponse> AddRole(AddRoleModel model)
        {
            var result = await _roleService.AddRole(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updaterole")]
        public async Task<IResponse> UpdateRole(UpdateRoleModel model)
        {
            var result = await _roleService.UpdateRole(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleterole/{id}")]
        public async Task<IResponse> DeleteRole(string id)
        {
            var result = await _roleService.DeleteRole(id);
            return result;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("getassignedroles/{userid}")]
        public async Task<IResponse> GetAssignedRoles(string userid)
        {
            var result = await _roleService.GetAssignedRoles(userid);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("roleassign")]
        public async Task<IResponse> RoleAssign(List<AssignRole> models)
        {
            var result = await _roleService.RoleAssign(models);
            return result;
        }
    }
}
