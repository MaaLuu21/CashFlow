namespace CashFlow.Domain.Repositories;
public interface IUnitOfWork
{
    //tratar as trnasações pro bd
    //se falar ele não da o commit e sim um rollback
    Task Commit();
}
