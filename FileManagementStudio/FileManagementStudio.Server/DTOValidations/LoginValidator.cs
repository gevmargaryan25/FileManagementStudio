using FileManagementStudio.Server.DTOs;
using FluentValidation;

namespace FileManagementStudio.Server.DTOValidations
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();       
        }
    }
}
