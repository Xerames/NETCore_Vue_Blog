using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class PasswordChangeByAdminValidator:AbstractValidator<PasswordChangeByAdminModel>
    {
        public PasswordChangeByAdminValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty();
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.NewPassword != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.NewPassword), "Passwords should match");
                }
            });
        }
    }
}
