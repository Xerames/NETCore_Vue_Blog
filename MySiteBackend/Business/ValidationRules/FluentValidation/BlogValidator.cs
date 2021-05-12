using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AddBlogValidator : AbstractValidator<CreateUpdateBlogModel>
    {
        public AddBlogValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(-1).WithMessage("Please select category.");
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.Image).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tagsids).NotNull();
        }
    }
    public class UpdateBlogValidator : AbstractValidator<CreateUpdateBlogModel>
    {
        public UpdateBlogValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CategoryId).GreaterThan(-1).WithMessage("Please select category.");
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tagsids).NotNull();
        }
    }
}
