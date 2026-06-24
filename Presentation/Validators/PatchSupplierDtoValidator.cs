using FluentValidation;
using Presentation.DTOs.Requests;

namespace Presentation.Validators
{
    public class PatchSupplierDtoValidator : AbstractValidator<PatchSupplierDto>
    {
        public PatchSupplierDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .SupplierNameRules()
                .When(x => x.Name is not null);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .AddressRules()
                .When(x => x.Address is not null);
        }
    }
}
