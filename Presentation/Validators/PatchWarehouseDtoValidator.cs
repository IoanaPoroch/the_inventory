using FluentValidation;
using Presentation.DTOs.Requests;

namespace Presentation.Validators
{
    public class PatchWarehouseDtoValidator : AbstractValidator<PatchWarehouseDto>
    {
        public PatchWarehouseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must not be empty.")
                .WarehouseNameRules()
                .When(x => x.Name is not null);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address must not be empty.")
                .AddressRules()
                .When(x => x.Address is not null);
        }
    }
}
