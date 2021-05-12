using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AddContactValidator:AbstractValidator<ContactViewModel>
    {
        public AddContactValidator()
        {
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Facebook).NotEmpty();
            RuleFor(x => x.Twitter).NotEmpty();
            RuleFor(x => x.Whatsapp).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Instagram).NotEmpty();
        }
    }
    public class UpdateContactValidator : AbstractValidator<ContactViewModel>
    {
        public UpdateContactValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Facebook).NotEmpty();
            RuleFor(x => x.Twitter).NotEmpty();
            RuleFor(x => x.Whatsapp).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Instagram).NotEmpty();
        }
    }
}
