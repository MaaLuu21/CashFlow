using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LogedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Expenses;
using CommonTestUtilities.requests;
using FluentAssertions;

namespace UseCases.Test.Expenses.Register;
public class ResgisterExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var logedUser = UserBuilder.Build();
        var request = RequestResgisterExpenseJsonBuilder.Build();
        var useCase = CreateUseCase(logedUser);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Title.Should().Be(request.Title);

    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        var logedUser = UserBuilder.Build();

        var request = RequestResgisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(logedUser);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.TITLE_REQUIRED));
    }

    private RegisterExpenseUseCase CreateUseCase(CashFlow.Domain.Entities.User user)
    {
        var repository = ExpensesWriteOnlyRepositoryBuilder.Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var logedUser = LogedUserBuilder.Build(user);

        return new RegisterExpenseUseCase(repository, unitOfWork, mapper, logedUser);

    }

}
