﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMinistrationDal : EfEntityRepositoryBase<Ministration, MySite2Context>, IMinistrationDal
    {
        public EfMinistrationDal(MySite2Context context) : base(context)
        {

        }
    }

}
