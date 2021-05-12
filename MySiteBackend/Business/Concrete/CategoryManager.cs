
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using Business.Abstract;
using Core.Utilities;
using Entities.Dtos;
using AutoMapper;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using Business.Constants;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Aspects.Autofac.Transaction;
using Core.Exceptions;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Exception;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;
        private IMapper _mapper;
        
        public CategoryManager(ICategoryDal categoryDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
        }

        public Category Get(int id)
        {
            return _categoryDal.Get(x => x.Id == id);
        }
        public List<Category> GetList()
        {
            return _categoryDal.GetList();
        }

        public List<Category> GetCategoriesWithBlogs()
        {
            return _categoryDal.GetCategoriesWithBlogs();
        }

        [ValidationAspect(typeof(AddCategoryValidator))]
        [LogAspect(typeof(MsSqlLogger))]
        public IResponse Add(CategoryViewModel model)
        {
            var exist = _categoryDal.Get(x => x.CategoryName.ToLower() == model.CategoryName.ToLower());
            if (exist != null)
            {
                throw new ApiException(400,Messages.CategoryNameIsAlreadyExist);
            }
            else
            {
                var category = _mapper.Map<Category>(model);
                category.Slug = SlugHelper.Slugify(model.CategoryName);
                _categoryDal.Add(category);
                return new DataResponse<Category>(category, 200,Messages.Added);
            }
        }
        [ValidationAspect(typeof(UpdateCategoryValidator))]
        [LogAspect(typeof(MsSqlLogger))]
        public IResponse Update(CategoryViewModel model)
        {
            var category = _categoryDal.Get(x => x.Id == model.Id);
            if (category == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else
            {
                _mapper.Map(model, category);
                category.Slug = SlugHelper.Slugify(model.CategoryName);
                _categoryDal.Update(category);
                return new SuccessResponse(200, Messages.Updated);
            }
        }

        [LogAspect(typeof(MsSqlLogger))]
        public IResponse Delete(int id)
        {
            var category = _categoryDal.Get(x => x.Id == id);
            if (category == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            _categoryDal.Delete(category);
            return new SuccessResponse(200, Messages.Deleted);
        }
    }
}