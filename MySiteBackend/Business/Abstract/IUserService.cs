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
    public interface IUserService
    {
        Task<IResponse> PasswordChange(string userid, PasswordChangeModel model);
        Task<IResponse> DeleteUser(string userid);
        Task<IResponse> DeleteUserPhoto(string userid);
        Task<IResponse> GetUser(string userid);
        Task<IResponse> GetUsersWithRoles();
        Task<IResponse> GetUserWithRoles(string userid);
        Task<IResponse> UpdateUserByAdmin(UserViewModel model);
        Task<IResponse> PasswordChangeByAdmin(PasswordChangeByAdminModel model);
        Task<IResponse> UpdateUser(string userid, UpdateUserModel model);
        Task<IResponse> AddOrUpdatePhoto(string userid, UserPhotoViewModel model);
        Task<IResponse> ForgetPassword(ForgetPasswordModel model);
        Task<IResponse> ResetPassword(ResetPasswordModel model);
        IResponse UploadImage(ImageUploadModel model);
        IResponse DeleteImage(DeleteImageModel model);

    }
}
