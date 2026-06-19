using FluentValidation;
using Presentation.DTOs.Requests;

namespace Presentation.Validators
{
    public class PatchProductDtoValidator : AbstractValidator<PatchProductDto>
    {
        public PatchProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must not be empty.")
                .NameRules()
                .When(x => x.Name is not null);

            RuleFor(x => x.Color)
                .ColorRules()
                .When(x => x.Color is not null);

            RuleFor(x => x.MadeIn)
                .NotEmpty().WithMessage("MadeIn must not be empty.")
                .MadeInRules()
                .When(x => x.MadeIn is not null);

            RuleFor(x => x.Price)
                .PriceRules()
                .When(x => x.Price is not null);

            RuleFor(x => x.WarehouseId)
                .WarehouseIdRules()
                .When(x => x.WarehouseId is not null);
        }
    }
}
