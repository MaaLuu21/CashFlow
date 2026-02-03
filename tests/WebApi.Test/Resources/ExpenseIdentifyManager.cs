using CashFlow.Domain.Entities;

namespace WebApi.Test.Resources;
public class ExpenseIdentifyManager
{
    private readonly Expense _expense;

    public ExpenseIdentifyManager(Expense expense)
    {
        _expense = expense;
    }

    public long GetId() => _expense.Id;
    public DateTime GetDate() => _expense.Date;   
}
