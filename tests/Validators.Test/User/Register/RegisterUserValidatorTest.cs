using MyRecipeBook.Application.UseCases.User.Register;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);

            //Assert
            result.IsValid == true;
        }
    }
}
