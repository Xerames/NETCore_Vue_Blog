
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IMinistrationService
    {
        Ministration Get(int id);
        List<Ministration> GetList();
        IResponse Add(MinistrationViewModel model);
        IResponse Update(MinistrationViewModel model);
        IResponse Delete(int id);
        IResponse UploadImage(ImageUploadModel model);
        IResponse DeleteImage(DeleteImageModel model);
    }
}
