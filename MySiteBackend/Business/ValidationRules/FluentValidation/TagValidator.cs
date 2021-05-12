using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AddTagValidator:AbstractValidator<TagCreateUpdateModel>
    {
        public AddTagValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
    public class UpdateTagValidator : AbstractValidator<TagCreateUpdateModel>
    {
        public UpdateTagValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
