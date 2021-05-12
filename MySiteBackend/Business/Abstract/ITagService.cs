
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ITagService
    {
        Tag Get(int id);
        List<Tag> GetList();
        IResponse Add(TagCreateUpdateModel model);
        IResponse Update(TagCreateUpdateModel model);
        IResponse Delete(int id);
        Tag GetTagWithBlogs(int id);
        IResponse DeleteBlogFromTag(int blogid,int tagid);
    }
}
