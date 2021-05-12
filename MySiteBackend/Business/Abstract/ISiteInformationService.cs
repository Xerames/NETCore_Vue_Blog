
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ISiteInformationService
    {
        SiteInformation Get(int id);
        List<SiteInformation> GetList();
        IResponse Add(SiteInformationViewModel model);
        IResponse Update(SiteInformationViewModel model);
        IResponse Delete(int id);
        IResponse UploadImage(ImageUploadModel model);
        IResponse DeleteImage(DeleteImageModel model);
    }
}
