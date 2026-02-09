
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Services.LogedUser;

namespace CashFlow.Application.UseCases.Users.Delete;
public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
{
    private readonly ILogedUser _logedUser;
    private readonly IUserWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserAccountUseCase(ILogedUser logedUser,
                                    IUserWriteOnlyRepository repository,
                                    IUnitOfWork unitOfWork)
    {
        _logedUser = logedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var user = await _logedUser.Get();

        await _repository.Delete(user);

        await _unitOfWork.Commit();
    }
}
