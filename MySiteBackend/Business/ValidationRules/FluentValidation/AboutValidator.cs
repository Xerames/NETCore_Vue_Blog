using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AddAboutValidator : AbstractValidator<AboutViewModel>
    {
        public AddAboutValidator()
        {
            RuleFor(about => about.Description).NotEmpty();
            RuleFor(about => about.Image).NotNull();
        }
    }

    public class UpdateAboutValidator : AbstractValidator<AboutViewModel>
    {
        public UpdateAboutValidator()
        {
            RuleFor(about => about.Id).NotEmpty();
            RuleFor(about => about.Description).NotEmpty();
        }
    }
}
