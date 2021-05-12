
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IContactService
    {
        Contact Get(int id);
        List<Contact> GetList();
        IResponse Add(ContactViewModel model);
        IResponse Update(ContactViewModel model);
        IResponse Delete(int id);
    }
}
