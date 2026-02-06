namespace CashFlow.Domain.Repositories.User;
public interface IUserUpdateOnlyRepository
{
    Task<Entities.User> GetById(long id);

    void Update(Domain.Entities.User user);
}
