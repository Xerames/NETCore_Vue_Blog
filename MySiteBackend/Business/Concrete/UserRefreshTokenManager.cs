
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using Business.Abstract;

namespace Business.Concrete
{
    public class UserRefreshTokenManager : IUserRefreshTokenService
    {
        private IUserRefreshTokenDal _userRefreshTokenDal;
        public UserRefreshTokenManager(IUserRefreshTokenDal userRefreshTokenDal)
        {
            _userRefreshTokenDal = userRefreshTokenDal;
        }

        public UserRefreshToken GetByCode(string code)
        {
            return _userRefreshTokenDal.Get(x => x.Code == code);
        }

        public UserRefreshToken GetByUserId(string userid)
        {
            return _userRefreshTokenDal.Get(x => x.UserId == userid);
        }


        public List<UserRefreshToken> GetList()
        {
            return _userRefreshTokenDal.GetList();
        }

        public void Add(UserRefreshToken entity)
        {
            _userRefreshTokenDal.Add(entity);
        }

        public void Update(UserRefreshToken entity)
        {
            _userRefreshTokenDal.Update(entity);
        }

        public void Delete(UserRefreshToken entity)
        {
            _userRefreshTokenDal.Delete(entity);
        }

    }

}