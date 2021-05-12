using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AddReplyValidator: AbstractValidator<ReplyViewModel>
    {
        public AddReplyValidator()
        {
            RuleFor(x => x.CommentId).NotEmpty();
            RuleFor(x => x.Content).NotNull().WithMessage("Reply must not be null");
            RuleFor(x => x.UserId).NotNull();
        }
    }
    public class UpdateReplyValidator : AbstractValidator<ReplyViewModel>
    {
        public UpdateReplyValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CommentId).NotNull();
            RuleFor(x => x.Content).NotNull().WithMessage("Reply must not be null");
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
