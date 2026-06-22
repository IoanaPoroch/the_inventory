using FluentValidation;
using Presentation.DTOs.Requests;

namespace Presentation.Validators
{
    public class CreateWarehouseDtoValidator : AbstractValidator<CreateWarehouseDto>
    {
        public CreateWarehouseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .WarehouseNameRules();

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .AddressRules();
        }
    }
}
