
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserRefreshTokenDal : EfEntityRepositoryBase<UserRefreshToken, MySite2Context>, IUserRefreshTokenDal
    {
        public EfUserRefreshTokenDal(MySite2Context context) : base(context)
        {

        }
    }
}
