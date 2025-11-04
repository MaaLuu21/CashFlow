using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

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
    public async Task Add(Expense expense)
    {
        //adiciona
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<List<Expense>> GetAll()
    {
        return await _dbContext.Expenses.AsNoTracking().ToListAsync();
        //AsNoTracking melhora a perfamace, bom usar antes de where, to list...
        // Apenas quando não for alterar algum valor
        //evita gastar memoria atoa e deixa mais rápido
        //sempre que cirar uma função no repositorio
        //que devolve dados se pergunte se quem utiliza a função
        //pode alterar dados nessa entidade se fr não use AsNoTracking        
    }
}
