using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LogedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories.Expenses;
using FluentAssertions;

namespace UseCases.Test.Expenses.GetAll;
public class GetAllExpensesUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var logedUser = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection(logedUser);

        var useCase = CreateUseCase(logedUser, expenses);

        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Expenses.Should().NotBeNullOrEmpty().And.AllSatisfy(expense =>
        {
            expense.Id.Should().BeGreaterThan(0);
            expense.Title.Should().NotBeNullOrEmpty();
            expense.Amount.Should().BeGreaterThan(0);
        });

    }

    private GetAllExpenseUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().GetAll(user, expenses).Build();
        var mapper = MapperBuilder.Build();
        var logedUser = LogedUserBuilder.Build(user);

        return new GetAllExpenseUseCase(repository, mapper, logedUser);
    }
}
