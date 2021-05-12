
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IReplyService
    {
        Reply Get(int id);
        List<Reply> GetList();
        IResponse Add(ReplyViewModel model);
        IResponse Update(ReplyViewModel model);
        IResponse UpdateReplyByAdmin(UpdateReplyByAdmin model);
        IResponse Delete(int id);
    }
}
