using FileManagementStudio.Server.DTOs;
using FluentValidation;

namespace FileManagementStudio.Server.DTOValidations
{
    public class RegValidator : AbstractValidator<RegisterDto>
    {
        public RegValidator()
        {
            RuleFor(x => x.Username)
             .NotNull()
             .NotEmpty();
            RuleFor(x => x.Password)
             .NotNull()
             .NotEmpty()
             .MustContainUpperCase();
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
