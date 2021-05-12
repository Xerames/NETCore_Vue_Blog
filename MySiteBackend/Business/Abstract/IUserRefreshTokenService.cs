
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserRefreshTokenService
    {
        UserRefreshToken GetByCode(string code);
        UserRefreshToken GetByUserId(string userid);
        List<UserRefreshToken> GetList();
        void Add(UserRefreshToken entity);
        void Update(UserRefreshToken entity);
        void Delete(UserRefreshToken entity);
    }
}
