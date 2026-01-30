using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LogedUser;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogedUser _logedUser;

    public RegisterExpenseUseCase(
        IExpensesWriteOnlyRepository repository, 
        IUnitOfWork unitOfWork,
        IMapper mapper, ILogedUser logedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logedUser = logedUser;
    }

    public async Task<ResponseRegisteredExpenseJson> Execute(RequestExpenseJson request)
    {
        //validações
        Validate(request);

        var logedUser = await _logedUser.Get();

        var expense = _mapper.Map<Expense>(request);
        expense.UserId = logedUser.Id;

        await _repository.Add(expense);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredExpenseJson>(expense);
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        var result = validator.Validate(request);


        if (result.IsValid == false)
        {
            //result.erros devolve a lista de erros que é uma lista de validationerros
            //seleciona em toda lista apenas o errormessage
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
