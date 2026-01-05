using CommonTestUtilities.Cryptogrphy;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User;
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

        private RegisterUserUseCase CreateUseCase()
        {
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var readRepository = new UserReadOnlyRepositoryBuilder().Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();

            return new RegisterUserUseCase(writeRepository, readRepository, unitOfWork, mapper, passwordEncripter);
        }
    }
}
