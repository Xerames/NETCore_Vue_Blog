using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AddSiteInformationValidator: AbstractValidator<SiteInformationViewModel>
    {
        public AddSiteInformationValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Logo).NotEmpty();
            RuleFor(x => x.Keywords).NotEmpty();
        }
    }
    public class UpdateSiteInformationValidator : AbstractValidator<SiteInformationViewModel>
    {
        public UpdateSiteInformationValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Keywords).NotEmpty();
        }
    }
}
