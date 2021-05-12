using System;
using System.Linq;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Core.DataAccess.EntityFramework;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfReplyDal : EfEntityRepositoryBase<Reply, MySite2Context>, IReplyDal
    {
        public EfReplyDal(MySite2Context context) : base(context)
        {

        }
    }
}
