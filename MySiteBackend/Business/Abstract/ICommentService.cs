
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICommentService
    {
        Comment Get(int id);
        List<Comment> GetList();
        IResponse Add(CommentViewModel model);
        IResponse Update(CommentViewModel model);
        IResponse Delete(int id);
        IResponse UpdateCommentByAdmin(UpdateCommentByAdmin model);
    }
}
