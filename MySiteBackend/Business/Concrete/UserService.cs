using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Exceptions;
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManager;
        private IMapper _mapper;
        private IEmailSender _emailSender;
        public UserService(UserManager<User> userManager, IMapper mapper, IEmailSender emailSender)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        [ValidationAspect(typeof(UserPhotoValidator))]
        public async Task<IResponse> AddOrUpdatePhoto(string userid, UserPhotoViewModel model)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else if (user.Photo == model.Photo)
            {
                _mapper.Map(model, user);
                await _userManager.UpdateAsync(user);
                return new SuccessResponse(200, Messages.Updated);
            }
            else if (user.Photo == null)
            {
                _mapper.Map(model, user);
                await _userManager.UpdateAsync(user);
                return new SuccessResponse(200, Messages.Updated);
            }
            FileManager.DeleteFile(user.Photo);
            _mapper.Map(model, user);
            await _userManager.UpdateAsync(user);
            return new SuccessResponse(200, Messages.Updated);
        }

        public async Task<IResponse> DeleteUser(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                if (user.Photo != null)
                {
                    FileManager.DeleteFile(user.Photo);
                }
                return new SuccessResponse(200, Messages.Deleted);
            }
            else
            {
                throw new ApiException(400, result.Errors.Select(e => e.Description).ToList());
            }
        }

        public async Task<IResponse> DeleteUserPhoto(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            if (user.Photo != null)
            {
                FileManager.DeleteFile(user.Photo);
                user.Photo = null;
            }
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new SuccessResponse(200, Messages.PhotoDeleted);
            }
            else
            {
                throw new ApiException(400, result.Errors.Select(e => e.Description).ToList());
            }
        }
        [ValidationAspect(typeof(ForgetPasswordValidator))]
        public async Task<IResponse> ForgetPassword(ForgetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            var tokenEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            string link = "http://localhost:8080/resetpassword/email/" + model.Email + "/token/" + tokenEncoded;
            await _emailSender.ForgetPasswordMailAsync(link, model.Email);
            return new SuccessResponse(200, Messages.IfEmailTrue);
        }

        public async Task<IResponse> GetUser(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            var mappeduser = _mapper.Map<UserViewModel>(user);
            return new DataResponse<UserViewModel>(mappeduser, 200);
        }

        public async Task<IResponse> GetUsersWithRoles()
        {
            var userswithroles = new List<UserWithRolesViewModel>();

            foreach (var user in _userManager.Users.ToList())
            {
                var mappeduser = _mapper.Map<UserWithPhotoViewModel>(user);
                userswithroles.Add(new UserWithRolesViewModel()
                {
                    User = mappeduser,
                    Roles = await _userManager.GetRolesAsync(user)
                });
            }
            return new DataResponse<List<UserWithRolesViewModel>>(userswithroles, 200);
        }

        public async Task<IResponse> GetUserWithRoles(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            var userroles = await _userManager.GetRolesAsync(user);
            var mappeduser = _mapper.Map<UserWithPhotoViewModel>(user);
            var userwithroles = new UserWithRolesViewModel() { User = mappeduser, Roles = userroles };
            return new DataResponse<UserWithRolesViewModel>(userwithroles, 200);
        }

        [ValidationAspect(typeof(PasswordChangeValidator))]
        public async Task<IResponse> PasswordChange(string userid, PasswordChangeModel model)
        {
            var user = await _userManager.FindByIdAsync(userid);
            bool currentpasswordtrue = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (currentpasswordtrue == true)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return new SuccessResponse(200, Messages.PasswordChangedSuccessfully);
                }
                else
                {
                    throw new ApiException(400, result.Errors.Select(e => e.Description).ToList());
                }
            }
            throw new ApiException(400, Messages.CurrentPasswordIsFalse);
        }
        [ValidationAspect(typeof(PasswordChangeByAdminValidator))]
        public async Task<IResponse> PasswordChangeByAdmin(PasswordChangeByAdminModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            if (result.Succeeded)
            {
                return new SuccessResponse(200, Messages.PasswordChangedSuccessfully);
            }
            throw new ApiException(400, result.Errors.Select(e => e.Description).ToList());
        }

        [ValidationAspect(typeof(ResetPasswordValidator))]
        public async Task<IResponse> ResetPassword(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                throw new ApiException(400, Messages.PasswordDontMatchWithConfirmation);
            }
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(model.Token);
            var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);
            var result = await _userManager.ResetPasswordAsync(user, tokenDecoded, model.NewPassword);
            if (result.Succeeded)
            {
                return new SuccessResponse(200, Messages.PasswordResetSuccessfully);
            }
            else
            {
                throw new ApiException(400, result.Errors.Select(e => e.Description).ToList());
            }
        }

        [ValidationAspect(typeof(UpdateUserValidator))]
        public async Task<IResponse> UpdateUser(string userid, UpdateUserModel model)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            var email = await _userManager.FindByEmailAsync(model.Email);
            if (email != null && user.Email != model.Email)
            {
                throw new ApiException(400, Messages.EmailIsAlreadyExist);
            }
            _mapper.Map(model, user);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new SuccessResponse(200, Messages.Updated);
            }
            else
            {
                throw new ApiException(400, result.Errors.Select(e => e.Description).ToList());
            }
        }
        [ValidationAspect(typeof(UpdateUserByAdminValidator))]
        public async Task<IResponse> UpdateUserByAdmin(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            _mapper.Map(model, user);
            var username = await _userManager.FindByNameAsync(model.UserName);
            if (username != null && user.UserName != model.UserName)
            {
                throw new ApiException(400, Messages.UsernameIsAlreadyExist);
            }
            var email = await _userManager.FindByEmailAsync(model.Email);
            if (email != null && user.Email != model.Email)
            {
                throw new ApiException(400, Messages.EmailIsAlreadyExist);
            }
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new SuccessResponse(200, Messages.Updated);
            }
            throw new ApiException(400, result.Errors.Select(e => e.Description).ToList());
        }

        [ValidationAspect(typeof(UploadImageValidator))]
        public IResponse UploadImage(ImageUploadModel model)
        {
            var imagepath = FileManager.SaveFile(FolderNames.Photos, model.Image);
            return new DataResponse<string>(imagepath, 200);
        }

        public IResponse DeleteImage(DeleteImageModel model)
        {
            FileManager.DeleteFile(model.Image);
            return new SuccessResponse(200, Messages.ImageDeleted);
        }
    }
}
