using FluentValidation;
using Presentation.DTOs.Requests;

namespace Presentation.Validators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .NameRules();

            RuleFor(x => x.Color)
                .ColorRules()
                .When(x => x.Color is not null);

            RuleFor(x => x.MadeIn)
                .NotEmpty().WithMessage("MadeIn is required.")
                .MadeInRules();

            RuleFor(x => x.Price)
                .PriceRules();

            RuleFor(x => x.WarehouseId)
                .WarehouseIdRules();
        }
    }
}
