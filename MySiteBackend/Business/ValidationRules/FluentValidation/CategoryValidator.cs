using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AddCategoryValidator : AbstractValidator<CategoryViewModel>
    {
        public AddCategoryValidator()
        {
            RuleFor(category => category.CategoryName).NotEmpty();
            RuleFor(category => category.Description).NotEmpty();
        }
    }
    public class UpdateCategoryValidator : AbstractValidator<CategoryViewModel>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(category => category.Id).NotEmpty();
            RuleFor(category => category.CategoryName).NotEmpty();
            RuleFor(category => category.Description).NotEmpty();
        }
    }
}
