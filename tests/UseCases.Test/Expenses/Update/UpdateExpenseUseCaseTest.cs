using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LogedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Expenses;
using CommonTestUtilities.requests;
using FluentAssertions;

namespace UseCases.Test.Expenses.Update;
public class UpdateExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var logedUser = UserBuilder.Build();
        var request = RequestExpenseJsonBuilder.Build();
        var expense = ExpenseBuilder.Build(logedUser);

        var useCase = CreateUseCase(logedUser, expense);

        var act = async () => await useCase.Execute(expense.Id, request);

        var result = await act.Should().NotThrowAsync();

        expense.Title.Should().Be(request.Title);
        expense.Description.Should().Be(request.Description);
        expense.Date.Should().Be(request.Date);
        expense.Amount.Should().Be(request.Amount);
        expense.PaymentType.Should().Be((CashFlow.Domain.Enums.PaymentsType)request.PaymentType);
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        var logedUser = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(logedUser);

        var request = RequestExpenseJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(logedUser);

        var act = async () => await useCase.Execute(id: 1000, request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.TITLE_REQUIRED));
    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var logedUser = UserBuilder.Build();

        var request = RequestExpenseJsonBuilder.Build();

        var useCase = CreateUseCase(logedUser);

        var act = async () => await useCase.Execute(id: 1000, request);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.EXPENSE_NOT_FOUND));
    }

    private UpdateExpenseUseCase CreateUseCase(User user, Expense? expense = null)
    {
        var repository = new ExpensesUpdateOnlyRepositoryBuilder().GetById(user, expense).Build();
        var mapper = MapperBuilder.Build();
        var unityOfWork = UnitOfWorkBuilder.Build();
        var logedUser = LogedUserBuilder.Build(user);

        return new UpdateExpenseUseCase(mapper, unityOfWork, repository, logedUser);
    }

}
