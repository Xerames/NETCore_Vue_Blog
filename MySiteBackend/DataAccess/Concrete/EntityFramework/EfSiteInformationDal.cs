
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSiteInformationDal : EfEntityRepositoryBase<SiteInformation, MySite2Context>, ISiteInformationDal
    {
        public EfSiteInformationDal(MySite2Context context) : base(context)
        {

        }
    }
}
