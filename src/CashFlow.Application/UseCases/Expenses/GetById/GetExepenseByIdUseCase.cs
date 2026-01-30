using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LogedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public class GetExepenseByIdUseCase : IGetExpenseByIdUseCase
{
    private readonly IExpensesReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogedUser _logedUser;

    public GetExepenseByIdUseCase(IExpensesReadOnlyRepository repository, 
                                  IMapper mapper,
                                  ILogedUser logedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _logedUser = logedUser;
    }

    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var logedUser = await _logedUser.Get();

        var result = await _repository.GetById(logedUser, id);

        if (result == null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        return _mapper.Map<ResponseExpenseJson>(result);
    }
}
