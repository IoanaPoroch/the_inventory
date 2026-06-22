using FluentValidation;

namespace Presentation.Validators
{
    public static class WarehouseValidationRules
    {
        public static IRuleBuilderOptions<T, string?> WarehouseNameRules<T>(this IRuleBuilder<T, string?> rule)
            => rule.MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        public static IRuleBuilderOptions<T, string?> AddressRules<T>(this IRuleBuilder<T, string?> rule)
            => rule.MaximumLength(200).WithMessage("Address must not exceed 200 characters.");
    }
}
