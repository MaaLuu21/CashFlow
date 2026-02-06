using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Application.UseCases.Users.Update;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LogedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.User;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Test.Users.Update;
public class UpdateUserUseCaseTest
{
    [Fact]
    public async Task Succes()
    {
        var user = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();

        user.Name.Should().Be(request.Name);
        user.Email.Should().Be(request.Email);
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var user = UserBuilder.Build();

        var request = RequestUpdateUserJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.NAME_EMPTY));
    }

    [Fact]
    public async Task Error_Email_Already_Exists()
    {
        var user = UserBuilder.Build();

        var request = RequestUpdateUserJsonBuilder.Build();

        var useCase = CreateUseCase(user, request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
    }

    private UpdateUserUseCase CreateUseCase(User user, string? email = null)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var updateRepository = UserUpdateOnlyRepositoryBuilder.Build(user);
        var logedUser = LogedUserBuilder.Build(user);
        var readRepository = new UserReadOnlyReporitoryBuilder();

        if(string.IsNullOrWhiteSpace(email) == false)
        {
            readRepository.ExistActiveUserWithEmail(email);
        }

        return new UpdateUserUseCase(logedUser, readRepository.Build(), updateRepository, unitOfWork);
    }
}
