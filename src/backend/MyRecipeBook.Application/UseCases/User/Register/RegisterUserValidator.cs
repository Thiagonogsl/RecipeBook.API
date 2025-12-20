using FluentValidation;
using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(user => user.Email).EmailAddress().WithMessage("Invalid email addres");
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage("Password must be greater than 6 characters");
        }
    }
}
