
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
    public class TagManager : ITagService
    {
        private ITagDal _tagDal;
        private IMapper _mapper;
        public TagManager(ITagDal tagDal, IMapper mapper)
        {
            _tagDal = tagDal;
            _mapper = mapper;
        }

        public Tag Get(int id)
        {
            return _tagDal.Get(x => x.Id == id);
        }

        public List<Tag> GetList()
        {
            return _tagDal.GetList();
        }

        [ValidationAspect(typeof(AddTagValidator))]
        public IResponse Add(TagCreateUpdateModel model)
        {
            var exist = _tagDal.Get(x => x.Name.ToLower() == model.Name.ToLower());
            if (exist != null)
            {
                throw new ApiException(400, Messages.TagNameIsAlreadyExist);
            }
            else
            {
                var tag = _mapper.Map<Tag>(model);
                tag.Slug = SlugHelper.Slugify(model.Name);
                _tagDal.Add(tag);
                return new DataResponse<Tag>(tag, 200,Messages.Added);
            }
        }

        [ValidationAspect(typeof(UpdateTagValidator))]
        public IResponse Update(TagCreateUpdateModel model)
        {
            var tag = _tagDal.Get(x => x.Id == model.Id);
            if (tag == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else
            {
                _mapper.Map(model, tag);
                tag.Slug = SlugHelper.Slugify(model.Name);
                _tagDal.Update(tag);
                return new SuccessResponse(200, Messages.Updated);
            } 
        }

        public IResponse Delete(int id)
        {
            var tag = _tagDal.Get(x => x.Id == id);
            if (tag == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            _tagDal.Delete(tag);
            return new SuccessResponse(200, Messages.Deleted);
        }

        public Tag GetTagWithBlogs(int id)
        {
            return _tagDal.GetTagWithBlogs(id);
        }

        public IResponse DeleteBlogFromTag(int blogid, int tagid)
        {
            _tagDal.DeleteBlogFromTag(blogid, tagid);
            return new SuccessResponse(200, Messages.Deleted);
        }
    }
}