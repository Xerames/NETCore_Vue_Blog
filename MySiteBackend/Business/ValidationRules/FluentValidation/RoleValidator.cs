using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AddRoleValidator:AbstractValidator<RoleViewModel>
    {
        public AddRoleValidator()
        {
            RuleFor(x => x.Name).NotNull();
        }
    }

    public class UpdateRoleValidator : AbstractValidator<RoleViewModel>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotNull();
        }
    }
}
