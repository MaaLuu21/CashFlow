using CashFlow.Domain.Entities;
using CashFlow.Domain.Services.LogedUser;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.LogedUser;
public class LogedUserBuilder
{
    public static ILogedUser Build(User user)
    {
        var mock = new Mock<ILogedUser>();

        mock.Setup(logedUser => logedUser.Get()).ReturnsAsync(user);

        return mock.Object;

    }
}
