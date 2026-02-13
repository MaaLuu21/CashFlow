using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LogedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Update;
public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExpensesUpdateOnlyRepository _repository;
    private readonly ILogedUser _logedUser;

    public UpdateExpenseUseCase(IMapper mapper, 
                                IUnitOfWork unitOfWork, 
                                IExpensesUpdateOnlyRepository repository, 
                                ILogedUser logedUser)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _logedUser = logedUser;
    }

    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);

        var logedUser = await _logedUser.Get();

        var expense = await _repository.GetById(logedUser, id);

        if (expense == null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        } 

        expense.Tags.Clear();

        _mapper.Map(request, expense);

        _repository.Update(expense);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();

        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }

    }



}
