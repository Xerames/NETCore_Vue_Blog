
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using Business.Abstract;
using Core.Utilities;
using Entities.Dtos;
using AutoMapper;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Business.ValidationRules.FluentValidation;
using FluentValidation;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using Business.Constants;
using Microsoft.AspNetCore.Http;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;
using Core.Exceptions;

namespace Business.Concrete
{
    public class AboutManager : IAboutService
    {
        private IAboutDal _aboutDal;
        private IMapper _mapper;
        public AboutManager(IAboutDal aboutDal, IMapper mapper)
        {
            _aboutDal = aboutDal;
            _mapper = mapper;
        }

        public About Get(int id)
        {
            return _aboutDal.Get(x => x.Id == id);
        }

        [CacheAspect]
        public List<About> GetList()
        {
            return _aboutDal.GetList();
        }


        [ValidationAspect(typeof(AddAboutValidator))]
        [LogAspect(typeof(MsSqlLogger))]
        [CacheRemoveAspect("IAboutService.Get")]
        public IResponse Add(AboutViewModel model)
        {
            var about = _mapper.Map<About>(model);
            _aboutDal.Add(about);
            return new DataResponse<About>(about, 200,Messages.Added);
        }
        [ValidationAspect(typeof(UpdateAboutValidator))]
        [LogAspect(typeof(MsSqlLogger))]
        [CacheRemoveAspect("IAboutService.Get")]
        public IResponse Update(AboutViewModel model)
        {
            var about = _aboutDal.Get(x => x.Id == model.Id);
            if (about == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else if (about.Image == model.Image)
            {
                _mapper.Map(model, about);
                _aboutDal.Update(about);
                return new SuccessResponse(200, Messages.Updated);
            }
            FileManager.DeleteFile(about.Image);
            _mapper.Map(model, about);
            _aboutDal.Update(about);
            return new SuccessResponse(200, Messages.Updated);
        }

        [LogAspect(typeof(MsSqlLogger))]
        [CacheRemoveAspect("IAboutService.Get")]
        public IResponse Delete(int id)
        {
            var about = _aboutDal.Get(x => x.Id == id);
            if (about == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            FileManager.DeleteFile(about.Image);
            _aboutDal.Delete(about);
            return new SuccessResponse(200, Messages.Deleted);
        }

        [ValidationAspect(typeof(UploadImageValidator))]
        public IResponse UploadImage(ImageUploadModel model)
        {
            var imagepath = FileManager.SaveFile(FolderNames.Abouts, model.Image);
            return new DataResponse<string>(imagepath, 200);
        }

        public IResponse DeleteImage(DeleteImageModel model)
        {
            FileManager.DeleteFile(model.Image);
            return new SuccessResponse(200, Messages.ImageDeleted);
        }
    }
}