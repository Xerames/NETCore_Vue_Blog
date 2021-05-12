using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class PasswordChangeValidator:AbstractValidator<PasswordChangeModel>
    {
        public PasswordChangeValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty();
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
