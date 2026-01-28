using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UserReadOnlyReporitoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyReporitoryBuilder()
    {
        _repository = new Mock<IUserReadOnlyRepository>();
    }

    public IUserReadOnlyRepository Build() => _repository.Object;

    public UserReadOnlyReporitoryBuilder GetUserByEmail(User user)
    {
        _repository
            .Setup(repo => repo.GetUserByEmail(user.Email))
            .ReturnsAsync(user);

        return this;
    }

}
