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
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private UserManager<User> _userManager;
        private IUserRefreshTokenService _userRefreshTokenService;
        private ITokenService _tokenService;
        private IMapper _mapper;
        private IEmailSender _emailSender;
        public AuthManager(UserManager<User> userManager, IMapper mapper,IEmailSender emailSender, IUserRefreshTokenService userRefreshTokenService,ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _userRefreshTokenService = userRefreshTokenService;
            _tokenService = tokenService;
        }

        [ValidationAspect(typeof(RegisterValidator))]
        public async Task<IResponse> Register(RegisterViewModel model)
        {
            var email = await _userManager.FindByEmailAsync(model.Email);
            if (email != null)
            {
                throw new ApiException(400, Messages.EmailIsAlreadyExist);
            }
            var username = await _userManager.FindByNameAsync(model.UserName);
            if (username != null)
            {
                throw new ApiException(400, Messages.UsernameIsAlreadyExist);
            }
            var user = _mapper.Map<User>(model);
            var IdentityResult = await _userManager.CreateAsync(user, model.Password);
            if (IdentityResult.Succeeded)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
                var tokenEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
                string link = "http://localhost:8080/confirmemail/userId/" + user.Id + "/token/" + tokenEncoded;
                await _emailSender.ConfirmationMailAsync(link, model.Email);
                return new SuccessResponse(200, Messages.RegisterSuccessfully);
            }
            else
            {
                throw new ApiException(400, IdentityResult.Errors.Select(e => e.Description).ToList());
            }
        }

        public async Task<IResponse> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                throw new ApiException(404, Messages.TokenOrUserNotFound);
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            if (user.EmailConfirmed)
            {
                throw new ApiException(400, Messages.AlreadyAccountConfirmed);
            }
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);
            var result = await _userManager.ConfirmEmailAsync(user, tokenDecoded);
            if (result.Succeeded)
            {
                return new SuccessResponse(200, Messages.SuccessfullyAccountConfirmed);
            }
            throw new ApiException(400, Messages.AccountDontConfirmed);
        }


        [ValidationAspect(typeof(LoginValidator))]
        public async Task<IResponse> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                throw new ApiException(404, Messages.UsernameNotFound);
            }
            if (!user.EmailConfirmed)
            {
                throw new ApiException(400, Messages.ConfirmYourAccount);
            }
            var identityResult = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!identityResult)
            {
                throw  new ApiException(400, Messages.InvalidUsernameorPassword);
            }
            var token = await  _tokenService.CreateToken(user);
            var userRefreshToken = _userRefreshTokenService.GetByUserId(user.Id);
            if (userRefreshToken == null)
            {
                 _userRefreshTokenService.Add(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
                _userRefreshTokenService.Update(userRefreshToken);
            }
            return new DataResponse<TokenDTO>(token, 200);
        }

        public async Task<IResponse> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken =  _userRefreshTokenService.GetByCode(refreshToken);
            if (existRefreshToken == null)
            {
                throw new ApiException(404, Messages.RefreshTokenNotFound);
            }
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            var tokenDto = await _tokenService.CreateToken(user);
            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;
            _userRefreshTokenService.Update(existRefreshToken);
            return new DataResponse<TokenDTO>(tokenDto, 200);
        }

        public IResponse Logout(string refreshToken)
        {
            var existRefreshToken = _userRefreshTokenService.GetByCode(refreshToken);
            if (existRefreshToken == null)
            {
                throw new ApiException(404, Messages.RefreshTokenNotFound);
            }
            _userRefreshTokenService.Delete(existRefreshToken);
            return new SuccessResponse(200,Messages.LogoutSuccessfull);
        }
    }
}
