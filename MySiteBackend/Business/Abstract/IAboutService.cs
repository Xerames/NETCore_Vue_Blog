
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IAboutService
    {
        About Get(int id);
        List<About> GetList();
        IResponse Add(AboutViewModel model);
        IResponse Update(AboutViewModel model);
        IResponse Delete(int id);
        IResponse UploadImage(ImageUploadModel model);
        IResponse DeleteImage(DeleteImageModel model);
    }
}
