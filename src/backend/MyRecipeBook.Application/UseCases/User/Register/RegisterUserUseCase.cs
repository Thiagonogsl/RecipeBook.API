using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User
{
    public class RegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            // Validate request
            Validate(request);

            //Mapear a request em uma entidade
            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();

            var user = autoMapper.Map<Domain.Entities.User>(request);

            //Criptografar a senha
            var passwordEncripter = new PasswordEncripter();

            var encryptedPassword = passwordEncripter.Encrypt(request.Password);

            //Salvar a entidade no banco de dados
            await _userWriteOnlyRepository.Add(user);

            //Returnar a resposta
            return new ResponseRegisteredUserJson
            {
                Name = request.Name
            };
        }

        private void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
