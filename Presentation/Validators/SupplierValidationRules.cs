using FluentValidation;

namespace Presentation.Validators
{
    public static class SupplierValidationRules
    {
        public static IRuleBuilderOptions<T, string?> SupplierNameRules<T>(this IRuleBuilder<T, string?> rule)
            => rule.MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }
}
