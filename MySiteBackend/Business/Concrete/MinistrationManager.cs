
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
using Core.Utilities.Responses.Concrete;
using Core.Utilities.Responses.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Validation;
using Core.Exceptions;

namespace Business.Concrete
{
    public class MinistrationManager : IMinistrationService
    {
        private IMinistrationDal _ministrationDal;
        private IMapper _mapper;
        public MinistrationManager(IMinistrationDal ministrationDal, IMapper mapper)
        {
            _ministrationDal = ministrationDal;
            _mapper = mapper;
        }

        public Ministration Get(int id)
        {
            return _ministrationDal.Get(x => x.Id == id);
        }

        public List<Ministration> GetList()
        {
            return _ministrationDal.GetList();
        }

        [ValidationAspect(typeof(AddMinistrationValidator))]
        public IResponse Add(MinistrationViewModel model)
        {
            var ministration = _mapper.Map<Ministration>(model);
            _ministrationDal.Add(ministration);
            return new DataResponse<Ministration>(ministration, 200,Messages.Added);
        }
        [ValidationAspect(typeof(UpdateMinistrationValidator))]
        public IResponse Update(MinistrationViewModel model)
        {
            var ministration = _ministrationDal.Get(x => x.Id == model.Id);
            if (ministration == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else if (ministration.Image == model.Image)
            {
                _mapper.Map(model, ministration);
                _ministrationDal.Update(ministration);
                return new SuccessResponse(200, Messages.Updated);
            }
            FileManager.DeleteFile(ministration.Image);
            _mapper.Map(model, ministration);
            _ministrationDal.Update(ministration);
            return new SuccessResponse(200, Messages.Updated);
        }

        public IResponse Delete(int id)
        {
            var ministration = _ministrationDal.Get(x => x.Id == id);
            if (ministration == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            FileManager.DeleteFile(ministration.Image);
            _ministrationDal.Delete(ministration);
            return new SuccessResponse(200, Messages.Deleted);
        }

        [ValidationAspect(typeof(UploadImageValidator))]
        public IResponse UploadImage(ImageUploadModel model)
        {
            var imagepath = FileManager.SaveFile(FolderNames.Ministrations, model.Image);
            return new DataResponse<string>(imagepath, 200);
        }

        public IResponse DeleteImage(DeleteImageModel model)
        {
            FileManager.DeleteFile(model.Image);
            return new SuccessResponse(200, Messages.ImageDeleted);
        }
    }

}