using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LogedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Expenses;
using FluentAssertions;
using PdfSharp.Drawing;

namespace UseCases.Test.Expenses.Delete;
public class DeleteExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var logedUSer = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(logedUSer);

        var useCase = CreateUseCase(logedUSer, expense);

        var act = async () => await useCase.Execute(expense.Id);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var logedUSer = UserBuilder.Build();

        var useCase = CreateUseCase(logedUSer);

        var act = async () => await useCase.Execute(id: 1000);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.EXPENSE_NOT_FOUND));

    }

    private DeleteExpenseUseCase CreateUseCase(User user, Expense? expense = null)
    {
        var repositoryWriteOnly = ExpensesWriteOnlyRepositoryBuilder.Build();
        var repository = new ExpensesReadOnlyRepositoryBuilder().GetById(user, expense).Build();
        var unityOfWork = UnitOfWorkBuilder.Build();
        var logedUser = LogedUserBuilder.Build(user);

        return new DeleteExpenseUseCase(repositoryWriteOnly, repository, unityOfWork, logedUser);
    }
}
