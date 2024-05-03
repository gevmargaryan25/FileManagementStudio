using FluentValidation;

namespace FileManagementStudio.Server.DTOValidations
{
    public static class ExtensionsForValidation
    {
        public static IRuleBuilderOptions<T, string> MustContainUpperCase<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(propertyValue => ContainsUpperCase(propertyValue))
                              .WithMessage("Password must contain at least one uppercase letter.");
        }
        private static bool ContainsUpperCase(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Any(char.IsUpper);
        }
    }
}
