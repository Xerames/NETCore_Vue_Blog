
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
    public class SliderManager : ISliderService
    {
        private ISliderDal _sliderDal;
        private IMapper _mapper;
        public SliderManager(ISliderDal sliderDal, IMapper mapper)
        {
            _sliderDal = sliderDal;
            _mapper = mapper;
        }

        public Slider Get(int id)
        {
            return _sliderDal.Get(x => x.Id == id);
        }

        public List<Slider> GetList()
        {
            return _sliderDal.GetList();
        }
        [ValidationAspect(typeof(AddSliderValidator))]
        public IResponse Add(SliderViewModel model)
        {
            var slider = _mapper.Map<Slider>(model);
            _sliderDal.Add(slider);
            return new DataResponse<Slider>(slider, 200,Messages.Added);
        }

        [ValidationAspect(typeof(UpdateSliderValidator))]
        public IResponse Update(SliderViewModel model)
        {
            var slider = _sliderDal.Get(x => x.Id == model.Id);
            if (slider == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else if (slider.Image == model.Image)
            {
                _mapper.Map(model, slider);
                _sliderDal.Update(slider);
                return new SuccessResponse(200, Messages.Updated);
            }
            FileManager.DeleteFile(slider.Image);
            _mapper.Map(model, slider);
            _sliderDal.Update(slider);
            return new SuccessResponse(200, Messages.Updated);
        }

        public IResponse Delete(int id)
        {
            var slider = _sliderDal.Get(x => x.Id == id);
            if (slider == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else
            {
                FileManager.DeleteFile(slider.Image);
                _sliderDal.Delete(slider);
                return new SuccessResponse(200, Messages.Deleted);
            }
        }

        [ValidationAspect(typeof(UploadImageValidator))]
        public IResponse UploadImage(ImageUploadModel model)
        {
            var imagepath = FileManager.SaveFile(FolderNames.Sliders, model.Image);
            return new DataResponse<string>(imagepath, 200);
        }

        public IResponse DeleteImage(DeleteImageModel model)
        {
            FileManager.DeleteFile(model.Image);
            return new SuccessResponse(200, Messages.ImageDeleted);
        }
    }
}