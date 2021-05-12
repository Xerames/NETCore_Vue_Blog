
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ISliderService
    {
        Slider Get(int id);
        List<Slider> GetList();
        IResponse Add(SliderViewModel model);
        IResponse Update(SliderViewModel model);
        IResponse Delete(int id);
        IResponse UploadImage(ImageUploadModel model);
        IResponse DeleteImage(DeleteImageModel model);
    }
}
