
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfContactDal : EfEntityRepositoryBase<Contact, MySite2Context>, IContactDal
    {
        public EfContactDal(MySite2Context context) : base(context)
        {

        }
    }
}
