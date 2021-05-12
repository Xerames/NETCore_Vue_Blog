
using System;
using System.Linq;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Core.DataAccess.EntityFramework;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, MySite2Context>, ICategoryDal
    {
        public EfCategoryDal(MySite2Context context) : base(context)
        {

        }
        public List<Category> GetCategoriesWithBlogs()
        {
           return _context.Categories.Include(x => x.Blogs).ToList();
        }
    }
}
