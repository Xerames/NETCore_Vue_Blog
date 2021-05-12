using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRoleService
    {
        Task<IResponse> AddRole(AddRoleModel model);
        Task<IResponse> UpdateRole(UpdateRoleModel model);
        Task<IResponse> DeleteRole(string id);
        IResponse ListRole();

        Task<IResponse> GetRole(string id);
        Task<IResponse> GetAssignedRoles(string userid);
        Task<IResponse> RoleAssign(List<AssignRole> models);
    }
}
