using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ResetPasswordValidator:AbstractValidator<ResetPasswordModel>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
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
