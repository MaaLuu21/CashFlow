using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

//continua como internal infrastructure pode enxergar domain (por meio de dependencia)
//e para garantir no projeto de api não utilize essa classe
internal class ExpensesRepository : IExpensesRepository
{
    private readonly CashFlowDbContext _dbContext;  

    public ExpensesRepository(CashFlowDbContext dbContext)
    {
       _dbContext = dbContext;
    }
    public void Add(Expense expense)
    {

        //adiciona
        _dbContext.Expenses.Add(expense);
        //salva no bd
        _dbContext.SaveChanges();
    }
}
