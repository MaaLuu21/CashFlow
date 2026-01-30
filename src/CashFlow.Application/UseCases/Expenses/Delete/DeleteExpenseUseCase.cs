using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LogedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using System.Runtime.CompilerServices;

namespace CashFlow.Application.UseCases.Expenses.Delete;
public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _writeRepository;
    private readonly IExpensesReadOnlyRepository _readRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogedUser _logedUser;


    public DeleteExpenseUseCase(IExpensesWriteOnlyRepository writeRepository, 
        IExpensesReadOnlyRepository readRepository, IUnitOfWork unitOfWork,
        ILogedUser logedUser)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _unitOfWork = unitOfWork;
        _logedUser = logedUser;
        
    }
    public async Task Execute(long id)
    {
        var logedUser = await _logedUser.Get();

        var expense = await _readRepository.GetById(logedUser, id);

        if (expense == null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        await _writeRepository.Delete(id);
        await _unitOfWork.Commit();

    }
}
