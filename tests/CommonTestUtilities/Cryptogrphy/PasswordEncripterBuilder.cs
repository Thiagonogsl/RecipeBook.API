using MyRecipeBook.Application.Services.Cryptography;

namespace CommonTestUtilities.Cryptogrphy
{
    public class PasswordEncripterBuilder
    {
        public static PasswordEncripter Build()
        {
            return new PasswordEncripter("abc123");
        }
    }
}
