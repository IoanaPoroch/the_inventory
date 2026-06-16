using FluentValidation;
using Presentation.DTOs.Requests;

namespace Presentation.Validators
{
    public class PaginationQueryValidator : AbstractValidator<PaginationQuery>
    {
        public PaginationQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1).WithMessage("Page must be at least 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize must be at least 1.")
                .LessThanOrEqualTo(100).WithMessage("PageSize must not exceed 100.");
        }
    }
}
