using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;
public static class DependencyInjectionExtension
{
    //this sobrescreve para que possa ser usado no progam como um metodo
    // do proprio IServiceCollection
    public static void AddInfrastructure(this IServiceCollection services)
    {
        AddDbContext(services);
        AddRespositories(services);
    }

    private static void AddRespositories(IServiceCollection services)
    {
        services.AddScoped<IExpensesRepository, ExpensesRepository>();
    }
    private static void AddDbContext(IServiceCollection services)
    {
        services.AddDbContext<CashFlowDbContext>();
    }
}
