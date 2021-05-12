using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Exceptions;
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RoleService : IRoleService
    {
        private RoleManager<Role> _roleManager;
        private UserManager<User> _userManager;
        private IMapper _mapper;
        public RoleService(RoleManager<Role> roleManager, IMapper mapper, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }
        [ValidationAspect(typeof(AddRoleValidator))]
        public async Task<IResponse> AddRole(AddRoleModel model)
        {
            var rolefromdb = await _roleManager.FindByNameAsync(model.Name);
            if (rolefromdb == null)
            {
                var role = _mapper.Map<Role>(model);
                var Identityresult = await _roleManager.CreateAsync(role);
                if (Identityresult.Succeeded)
                {
                    return new DataResponse<Role>(role, 200,Messages.Added);
                }
                throw new ApiException(400, Identityresult.Errors.Select(x => x.Description).ToString());
            }
            throw new ApiException(400, Messages.RoleNameIsAlreadyExist);
        }

        public async Task<IResponse> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return new SuccessResponse(200, Messages.Deleted);
            }
            throw new ApiException(400, result.Errors.Select(x => x.Description).ToString());
        }

        public async Task<IResponse> GetAssignedRoles(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            IQueryable<Role> roles = _roleManager.Roles;
            List<string> userroles = _userManager.GetRolesAsync(user).Result as List<string>;
            List<AssignedRole> AssignedRole = new List<AssignedRole>();
            foreach (var role in roles)
            {
                AssignedRole r = new AssignedRole();
                r.UserId = user.Id;
                r.RoleId = role.Id;
                r.RoleName = role.Name;
                if (userroles.Contains(role.Name))
                {
                    r.Exist = true;
                }
                else
                {
                    r.Exist = false;
                }
                AssignedRole.Add(r);
            }
            return new DataResponse<List<AssignedRole>>(AssignedRole, 200);
        }

        public async Task<IResponse> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var mappedrole = _mapper.Map<RoleViewModel>(role);
            return new DataResponse<RoleViewModel>(mappedrole, 200);
        }

        public IResponse ListRole()
        {
            var roles = _roleManager.Roles.ToList();
            return new DataResponse<List<Role>>(roles, 200);
        }

        public async Task<IResponse> RoleAssign(List<AssignRole> models)
        {
            foreach (var item in models)
            {
                var user = await _userManager.FindByIdAsync(item.UserId);
                if (item.Exist)

                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return new SuccessResponse(200, Messages.RoleAssignedSuccessfully);
        }

        [ValidationAspect(typeof(UpdateRoleValidator))]
        public async Task<IResponse> UpdateRole(UpdateRoleModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            _mapper.Map(model, role);
            var Identityresult = await _roleManager.UpdateAsync(role);
            if (Identityresult.Succeeded)
            {
                return new SuccessResponse(200, Messages.Updated);
            }
            throw new ApiException(400, Messages.RoleNameIsAlreadyExist);
        }
    }
}
