using CommonTestUtilities.Cryptogrphy;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(request.Name);
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            Func<Task> act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.ShouldSatisfyAllConditions(
                e => e.ErrorMessages.ShouldHaveSingleItem(),
                e => e.ErrorMessages.ShouldContain(ResourceMessagesException.EMAIL_ALREADY_REGISTERED)
                );
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase();

            Func<Task> act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.ShouldSatisfyAllConditions(
                e => e.ErrorMessages.ShouldHaveSingleItem(),
                e => e.ErrorMessages.ShouldContain(ResourceMessagesException.NAME_EMPTY)
                );
        }

        private RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

            if (string.IsNullOrEmpty(email) == false)
                readRepositoryBuilder.ExistActiveUserWithEmail(email);

            return new RegisterUserUseCase(writeRepository, readRepositoryBuilder.Build(), unitOfWork, mapper, passwordEncripter);
        }
    }
}
