using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LogedUser;
using CommonTestUtilities.Repositories.Expenses;
using FluentAssertions;

namespace UseCases.Test.Expenses.Reports.Pdf;
public class GenerateExpensesReportPdfUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var logedUser = UserBuilder.Build();

        var expenses = ExpenseBuilder.Collection(logedUser);

        var useCase = CreateUseCase(logedUser, expenses);

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Sucess_Empty()
    {
        var logedUser = UserBuilder.Build();

        var useCase = CreateUseCase(logedUser, new List<Expense>());

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().BeEmpty();
    }

    private GenerateExpensesReportPdfUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();
        var logedUser = LogedUserBuilder.Build(user);

        return new GenerateExpensesReportPdfUseCase(repository, logedUser);
    }
}
