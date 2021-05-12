
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Category Get(int id);
        List<Category> GetList();
        IResponse Add(CategoryViewModel model);
        IResponse Update(CategoryViewModel model);
        IResponse Delete(int id);
        List<Category> GetCategoriesWithBlogs();
        
    }
}
