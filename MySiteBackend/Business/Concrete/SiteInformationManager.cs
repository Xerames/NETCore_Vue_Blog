
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
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using Business.Constants;
using Core.Aspects.Autofac.Validation;
using Core.Exceptions;

namespace Business.Concrete
{
    public class SiteInformationManager : ISiteInformationService
    {
        private ISiteInformationDal _siteInformationDal;
        private IMapper _mapper;
        public SiteInformationManager(ISiteInformationDal siteInformationDal,IMapper mapper)
        {
            _siteInformationDal = siteInformationDal;
            _mapper = mapper;
        }

        public SiteInformation Get(int id)
        {
            return _siteInformationDal.Get(x => x.Id == id);
        }

        public List<SiteInformation> GetList()
        {
            return _siteInformationDal.GetList();
        }

        [ValidationAspect(typeof(AddSiteInformationValidator))]
        public IResponse Add(SiteInformationViewModel model)
        {
            var siteinformation = _mapper.Map<SiteInformation>(model);
            _siteInformationDal.Add(siteinformation);
            return new DataResponse<SiteInformation>(siteinformation, 200,Messages.Added);
        }

        [ValidationAspect(typeof(UpdateSiteInformationValidator))]
        public IResponse Update(SiteInformationViewModel model)
        {
            var siteinformation = _siteInformationDal.Get(x => x.Id == model.Id);
            if (siteinformation == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else if (siteinformation.Logo == model.Logo)
            {
                _mapper.Map(model, siteinformation);
                _siteInformationDal.Update(siteinformation);
                return new SuccessResponse(200, Messages.Updated);
            }
            FileManager.DeleteFile(siteinformation.Logo);
            _mapper.Map(model, siteinformation);
            _siteInformationDal.Update(siteinformation);
            return new SuccessResponse(200, Messages.Updated);
        }

        public IResponse Delete(int id)
        {
            var siteinformation = _siteInformationDal.Get(x => x.Id == id);
            if (siteinformation == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            FileManager.DeleteFile(siteinformation.Logo);
            _siteInformationDal.Delete(siteinformation);
            return new SuccessResponse(200, Messages.Deleted);
        }
        [ValidationAspect(typeof(UploadImageValidator))]
        public IResponse UploadImage(ImageUploadModel model)
        {
            var imagepath = FileManager.SaveFile(FolderNames.SiteInformations, model.Image);
            return new DataResponse<string>(imagepath, 200);
        }

        public IResponse DeleteImage(DeleteImageModel model)
        {
            FileManager.DeleteFile(model.Image);
            return new SuccessResponse(200, Messages.ImageDeleted);
        }
    }

}