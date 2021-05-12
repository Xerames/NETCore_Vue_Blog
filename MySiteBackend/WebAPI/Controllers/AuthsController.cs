using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private IAuthService _authService;
        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IResponse> Register(RegisterViewModel model)
        {
            var result = await _authService.Register(model);
            return result;
        }

        [HttpPost("confirmemail")]
        public async Task<IResponse> ConfirmEmail(ConfirmEmailModel model)
        {
            var result = await _authService.ConfirmEmail(model.userId, model.token);
            return result;
        }

        [HttpPost("login")]
        public async Task<IResponse> Login(LoginViewModel model)
        {
            var result = await _authService.Login(model);
            return result;
        }

        [HttpPost("refreshtoken")]
        public async Task<IResponse> RefreshToken(RefreshTokenModel model)
        {
            var result = await _authService.CreateTokenByRefreshToken(model.RefreshToken);
            return result;
        }


        [Authorize]
        [HttpPost("logout")]
        public IResponse Logout(RefreshTokenModel model)
        {
            var result =  _authService.Logout(model.RefreshToken);
            return result;
        }
    }
}
