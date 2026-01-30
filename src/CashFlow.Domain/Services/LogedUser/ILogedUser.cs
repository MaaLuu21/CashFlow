using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Services.LogedUser;
public interface ILogedUser
{
    Task<User> Get();
}
