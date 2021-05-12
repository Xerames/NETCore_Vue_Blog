
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
using Core.Aspects.Autofac.Caching;

namespace Business.Concrete
{
    public class ContactManager : IContactService
    {
        private IContactDal _contactDal;
        private IMapper _mapper;
        public ContactManager(IContactDal contactDal, IMapper mapper)
        {
            _contactDal = contactDal;
            _mapper = mapper;
        }

        public Contact Get(int id)
        {
            return _contactDal.Get(x => x.Id == id);
        }
	[CacheAspect]
        public List<Contact> GetList()
        {
            return _contactDal.GetList();
        }
        [ValidationAspect(typeof(AddContactValidator))]
	[CacheRemoveAspect("IContactService.Get")]
        public IResponse Add(ContactViewModel model)
        {
            var contact = _mapper.Map<Contact>(model);
            _contactDal.Add(contact);
            return new DataResponse<Contact>(contact, 200,Messages.Added);
        }

        [ValidationAspect(typeof(UpdateContactValidator))]
	[CacheRemoveAspect("IContactService.Get")]
        public IResponse Update(ContactViewModel model)
        {
            var contact = _contactDal.Get(x => x.Id == model.Id);
            if (contact == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            else
            {
                _mapper.Map(model, contact);
                _contactDal.Update(contact);
                return new SuccessResponse(200, Messages.Updated);
            }
        }
	[CacheRemoveAspect("IContactService.Get")]
        public IResponse Delete(int id)
        {
            var contact = _contactDal.Get(x => x.Id == id);
            if (contact == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            _contactDal.Delete(contact);
            return new SuccessResponse(200, Messages.Deleted);
        }
    }
}