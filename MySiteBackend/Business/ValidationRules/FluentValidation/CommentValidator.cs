using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AddCommentValidator: AbstractValidator<CommentViewModel>
    {
        public AddCommentValidator()
        {
            RuleFor(x => x.BlogId).GreaterThan(-1);
            RuleFor(x => x.Content).NotNull().WithMessage("Comment must not be null");
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
    public class UpdateCommentValidator : AbstractValidator<CommentViewModel>
    {
        public UpdateCommentValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.BlogId).GreaterThan(-1);
            RuleFor(x => x.Content).NotNull().WithMessage("Comment must not be null");
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
