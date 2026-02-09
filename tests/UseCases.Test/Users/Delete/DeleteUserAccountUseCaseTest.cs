using CashFlow.Application.UseCases.Users.Delete;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LogedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.User;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Test.Users.Delete;
public class DeleteUserAccountUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute();

        await act.Should().NotThrowAsync();
    }

    private DeleteUserAccountUseCase CreateUseCase(User user)
    {
        var repository = UserWriteOnlyRepositoryBuilder.Build();
        var logedUser = LogedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new DeleteUserAccountUseCase(logedUser, repository, unitOfWork);
    }
}
