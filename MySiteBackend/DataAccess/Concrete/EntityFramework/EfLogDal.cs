
using System;
using System.Linq;
using DataAccess.Concrete.EntityFramework.Contexts;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfLogDal : EfEntityRepositoryBase<Log, MySite2Context>, ILogDal
    {
        public EfLogDal(MySite2Context context) : base(context)
        {

        }
    }
}
