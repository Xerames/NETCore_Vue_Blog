using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AddMinistrationValidator: AbstractValidator<MinistrationViewModel>
    {
        public AddMinistrationValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Image).NotNull();
        }
    }
    public class UpdateMinistrationValidator : AbstractValidator<MinistrationViewModel>
    {
        public UpdateMinistrationValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
