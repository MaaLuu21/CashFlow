using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography;
public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build()
    {
        var mock = new Mock<IPasswordEncripter>();

        mock.Setup(config => config.Encrypt(It.IsAny<string>())).Returns("!Sdkf%jerlfj2323") ;

        return mock.Object;
    }
}
