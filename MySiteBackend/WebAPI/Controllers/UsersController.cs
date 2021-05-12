using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController( IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("getuserwithroles")]
        public async Task<IResponse> GetUserWithRoles()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userwithroles = _userService.GetUserWithRoles(userid);
            return await userwithroles;
        }

        [Authorize(Roles="Admin")]
        [HttpGet("getuser/{userid}")]
        public async Task<IResponse> GetUser(string userid)
        {
            var user = await _userService.GetUser(userid);
            return user;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getuserswithroles")]
        public async Task<IResponse> GetUsersWithRoles()
        {
            var userswithroles = _userService.GetUsersWithRoles();
            return await userswithroles;
        }

        [Authorize]
        [HttpPut("updateuser")]
        public async Task<IResponse> UpdateUser(UpdateUserModel model)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _userService.UpdateUser(userid, model);
            return result;
        }

        [Authorize]
        [HttpPost("deleteuserphoto")]
        public async Task<IResponse> DeletePhoto()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _userService.DeleteUserPhoto(userid);
            return result;
        }


        [Authorize]
        [HttpPost("passwordchange")]
        public async Task<IResponse> PasswordChange(PasswordChangeModel model)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _userService.PasswordChange(userid, model);
            return result;
        }
        [Authorize]
        [HttpPost("addorupdatephoto")]
        public async Task<IResponse> AddorUpdatePhoto(UserPhotoViewModel model)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _userService.AddOrUpdatePhoto(userid, model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteuser/{userid}")]
        public async Task<IResponse> DeleteUser(string userid)
        {
            var result = await _userService.DeleteUser(userid);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateuserbyadmin")]
        public async Task<IResponse> UpdateUserByAdmin(UserViewModel model)
        {
            var result = await _userService.UpdateUserByAdmin(model);
            return result;
        }

        [HttpPost("forgetpassword")]
        public async Task<IResponse> ForgetPassword(ForgetPasswordModel model)
        {
            var result = await _userService.ForgetPassword(model);
            return result;
        }
        [HttpPost("resetpassword")]
        public async Task<IResponse> ResetPassword(ResetPasswordModel model)
        {
            var result = await _userService.ResetPassword(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("passwordchangebyadmin")]
        public async Task<IResponse> PasswordChangeByAdmin(PasswordChangeByAdminModel model)
        {
            var result = await _userService.PasswordChangeByAdmin(model);
            return result;
        }


        [Authorize]
        [HttpPost("uploadimage")]
        public IResponse UploadImage([FromForm] ImageUploadModel model)
        {
            var result = _userService.UploadImage(model);
            return result;
        }

        [Authorize]
        [HttpPost("deleteimage")]
        public IResponse DeleteImage(DeleteImageModel model)
        {
            var result = _userService.DeleteImage(model);
            return result;
        }
    }
}
