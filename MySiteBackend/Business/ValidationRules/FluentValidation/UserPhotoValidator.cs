using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserPhotoValidator:AbstractValidator<UserPhotoViewModel>
    {
        public UserPhotoValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
        }
    }
}
