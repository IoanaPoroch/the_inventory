
using FluentValidation;

namespace Presentation.Validators
{
    public static class ProductValidationRules
    {
        public static IRuleBuilderOptions<T, string> NameRules<T>(this IRuleBuilder<T, string> rule)
            => rule.MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        public static IRuleBuilderOptions<T, string?> ColorRules<T>(this IRuleBuilder<T, string?> rule)
            => rule.MaximumLength(50).WithMessage("Color must not exceed 50 characters.");

        public static IRuleBuilderOptions<T, string> MadeInRules<T>(this IRuleBuilder<T, string> rule)
            => rule.MaximumLength(100).WithMessage("MadeIn must not exceed 100 characters.");

        public static IRuleBuilderOptions<T, decimal> PriceRules<T>(this IRuleBuilder<T, decimal> rule)
            => rule.GreaterThan(0).WithMessage("Price must be greater than 0.");

        public static IRuleBuilderOptions<T, decimal?> PriceRules<T>(this IRuleBuilder<T, decimal?> rule)
            => rule.GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}
