using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UploadImageValidator: AbstractValidator<ImageUploadModel>
    {
        public UploadImageValidator()
        {
            RuleFor(model => model.Image).NotNull();
        }
    }
}
