using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Services.LogedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Update;
public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly ILogedUser _logedUser;
    private readonly IUserReadOnlyRepository _readRepository;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(ILogedUser logedUser,
                             IUserReadOnlyRepository readRepository,
                             IUserUpdateOnlyRepository repository,
                             IUnitOfWork unitOfWork)
    {
        _logedUser = logedUser;
        _readRepository = readRepository;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestUpdateUserJson request)
    {
        var logedUser = await _logedUser.Get();

        await Validate(request, logedUser.Email);

        var user = await _repository.GetById(logedUser.Id);

        user.Name = request.Name;
        user.Email = request.Email;

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUserJson request, string currentEmail)
    {
        var validator = new UpdateUserValidator();

        var result = validator.Validate(request);

        if (currentEmail.Equals(request.Email) == false)
        {
            var userExist = await _readRepository.ExistActiveUserWithEmail(request.Email);
            if (userExist)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
            }
        }

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
 