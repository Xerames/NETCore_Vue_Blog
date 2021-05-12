
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
    public class ReplyManager : IReplyService
    {
        private IReplyDal _replyDal;
        private IMapper _mapper;
        public ReplyManager(IReplyDal replyDal, IMapper mapper)
        {
            _replyDal = replyDal;
            _mapper = mapper;
        }
        public Reply Get(int id)
        {
            return _replyDal.Get(x => x.Id == id);
        }

        public List<Reply> GetList()
        {
            return _replyDal.GetList();
        }
        [ValidationAspect(typeof(AddReplyValidator))]
        public IResponse Add(ReplyViewModel model)
        {
            var reply = _mapper.Map<Reply>(model);
            reply.ReplyDate = DateTime.Now;
            reply.Confirmation = true;
            _replyDal.Add(reply);
            return new DataResponse<Reply>(reply, 200,Messages.Added);
        }
        [ValidationAspect(typeof(UpdateReplyValidator))]
        public IResponse Update(ReplyViewModel model)
        {
            var reply = _replyDal.Get(x => x.Id == model.Id);
            if (reply == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else 
            {
                _mapper.Map(model, reply);
                reply.ReplyDate = DateTime.Now;
                reply.Confirmation = true;
                _replyDal.Update(reply);
                return new SuccessResponse(200, Messages.Updated);
            }
        }
        public IResponse Delete(int id)
        {
            var reply = _replyDal.Get(x => x.Id == id);
            if (reply == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            _replyDal.Delete(reply);
            return new SuccessResponse(200, Messages.Deleted);
        }

        public IResponse UpdateReplyByAdmin(UpdateReplyByAdmin model)
        {
            var reply = _replyDal.Get(x => x.Id == model.Id);
            if (reply == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else 
            {
                _mapper.Map(model, reply);
                _replyDal.Update(reply);
                return new SuccessResponse(200, Messages.Updated);
            }
        }
    }
}