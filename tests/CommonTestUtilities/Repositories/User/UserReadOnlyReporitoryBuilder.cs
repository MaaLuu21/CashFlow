using CashFlow.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories.User;
public class UserReadOnlyReporitoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyReporitoryBuilder()
    {
        _repository = new Mock<IUserReadOnlyRepository>();
    }

    public IUserReadOnlyRepository Build() => _repository.Object;

    public void ExistActiveUserWithEmail(string email)
    {
        _repository.Setup(user => user.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
    }

    public UserReadOnlyReporitoryBuilder GetUserByEmail(CashFlow.Domain.Entities.User user)
    {
        _repository
            .Setup(repo => repo.GetUserByEmail(user.Email))
            .ReturnsAsync(user);

        return this;
    }

}
