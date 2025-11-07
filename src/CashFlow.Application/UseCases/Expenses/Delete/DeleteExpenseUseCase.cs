using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using System.Runtime.CompilerServices;

namespace CashFlow.Application.UseCases.Expenses.Delete;
public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _writeRepository;
    private readonly IExpensesReadOnlyRepository _readRepository;
    private readonly IUnitOfWork _unitOfWork;


    public DeleteExpenseUseCase(IExpensesWriteOnlyRepository writeRepository, 
        IExpensesReadOnlyRepository readRepository, IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _unitOfWork = unitOfWork;
        
    }
    public async Task Execute(long id)
    {
        var expense = await _readRepository.GetById(id);

        if (expense == null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        _writeRepository.Delete(expense);
        _unitOfWork.Commit();

    }
}
