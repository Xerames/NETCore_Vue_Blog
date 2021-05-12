
using System;
using System.Linq;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Core.DataAccess.EntityFramework;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAboutDal : EfEntityRepositoryBase<About,MySite2Context>, IAboutDal
    {
        public EfAboutDal(MySite2Context context) : base(context)
        {

        }
    }
}
