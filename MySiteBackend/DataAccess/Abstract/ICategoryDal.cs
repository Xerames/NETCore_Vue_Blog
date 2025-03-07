﻿
using System;
using System.Collections.Generic;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        List<Category> GetCategoriesWithBlogs();
    }
}
