using FluentValidation;
using Presentation.DTOs.Requests;

namespace Presentation.Validators
{
    public class PatchProductDtoValidator : AbstractValidator<PatchProductDto>
    {
        public PatchProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .NameRules()
                .When(x => x.Name is not null);

            RuleFor(x => x.Color)
                .ColorRules()
                .When(x => x.Color is not null);

            RuleFor(x => x.MadeIn)
                .NotEmpty().WithMessage("MadeIn is required.")
                .MadeInRules()
                .When(x => x.MadeIn is not null);

            RuleFor(x => x.Price)
                .PriceRules()
                .When(x => x.Price is not null);

            RuleFor(x => x.WarehouseId)
                .WarehouseIdRules()
                .When(x => x.WarehouseId is not null);

            RuleFor(x => x.SupplierId)
                .SupplierIdRules()
                .When(x => x.SupplierId is not null);
        }
    }
}
