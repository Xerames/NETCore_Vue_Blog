
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using Business.Abstract;
using Entities.Dtos;
using Core.Utilities;
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
    public class CommentManager : ICommentService
    {
        private ICommentDal _commentDal;
        private IMapper _mapper;
        public CommentManager(ICommentDal commentDal, IMapper mapper)
        {
            _commentDal = commentDal;
            _mapper = mapper;
        }

        public Comment Get(int id)
        {
            return _commentDal.Get(x => x.Id == id);
        }

        public List<Comment> GetList()
        {
            return _commentDal.GetList();
        }
        [ValidationAspect(typeof(AddCommentValidator))]
        public IResponse Add(CommentViewModel model)
        {
            var comment = _mapper.Map<Comment>(model);
            comment.CommentDate = DateTime.Now;
            comment.Confirmation = true;
            _commentDal.Add(comment);
            return new DataResponse<Comment>(comment, 200,Messages.Added);
        }
        [ValidationAspect(typeof(UpdateCommentValidator))]
        public IResponse Update(CommentViewModel model)
        {
            var comment = _commentDal.Get(x => x.Id == model.Id);
            if (comment == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else
            {
                _mapper.Map(model, comment);
                comment.CommentDate = DateTime.Now;
                comment.Confirmation = true;
                _commentDal.Update(comment);
                return new SuccessResponse(200, Messages.Updated);
            }
        }
        public IResponse Delete(int id)
        {
            var comment = _commentDal.Get(x => x.Id == id);
            if (comment == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else
            {
                _commentDal.Delete(comment);
                return new SuccessResponse(200, Messages.Deleted);
            }
        }
        public IResponse UpdateCommentByAdmin(UpdateCommentByAdmin model)
        {
            var comment = _commentDal.Get(x => x.Id == model.Id);
            if (comment == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else 
            {
                _mapper.Map(model, comment);
                _commentDal.Update(comment);
                return new SuccessResponse(200, Messages.Updated);
            }
        }
    }
}