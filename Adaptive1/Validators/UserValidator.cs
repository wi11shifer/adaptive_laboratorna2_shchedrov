using FluentValidation;
using Adaptive1.Models;

namespace Adaptive1.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
