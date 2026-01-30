using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LogedUser;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public class GetAllExpenseUseCase : IGetAllExpenseUseCase
{
    private readonly IExpensesReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogedUser _logedUser;

    public GetAllExpenseUseCase(IExpensesReadOnlyRepository repository, 
                                IMapper mapper,
                                ILogedUser logedUser)
    {
       _repository = repository;
       _mapper = mapper;
       _logedUser = logedUser;
    }
    public async Task<ResponseExpensesJson> Execute()
    {
        var logedUser = await _logedUser.Get();

        var result = await _repository.GetAll(logedUser);

        return new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    }
}
