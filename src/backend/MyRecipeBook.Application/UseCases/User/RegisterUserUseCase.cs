using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.User
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
        {
            // Validate request

            //Mapear a request em uma entidade

            //Criptografar a senha

            //Salvar a entidade no banco de dados
            return new ResponseRegisteredUserJson
            {
                Name = request.Name
            };
        }
    }
}
