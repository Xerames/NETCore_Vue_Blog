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
    public interface IAuthService
    {
        Task<IResponse> Register(RegisterViewModel model);
        Task<IResponse> Login(LoginViewModel model);
        Task<IResponse> ConfirmEmail(string userId, string token);
        Task<IResponse> CreateTokenByRefreshToken(string refreshToken);
        IResponse Logout(string refreshToken);
    }
}
