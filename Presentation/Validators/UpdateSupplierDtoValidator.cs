using FluentValidation;
using Presentation.DTOs.Requests;

namespace Presentation.Validators
{
    public class UpdateSupplierDtoValidator : AbstractValidator<UpdateSupplierDto>
    {
        public UpdateSupplierDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .SupplierNameRules();

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .AddressRules();
        }
    }
}
