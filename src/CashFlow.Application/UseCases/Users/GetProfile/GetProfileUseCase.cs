
using AutoMapper;
using CashFlow.Domain.Services.LogedUser;

namespace CashFlow.Application.UseCases.Users.GetProfile;
public class GetProfileUseCase : IGetProfileUseCase
{
    private readonly ILogedUser _logedUser;
    private readonly IMapper _mapper;

    public GetProfileUseCase(ILogedUser logedUser, IMapper mapper)
    {
        _logedUser = logedUser;
        _mapper = mapper;
    }

    public async Task<ResponseUserProfileJson> Execute()
    {
        var user = await _logedUser.Get();

        return _mapper.Map<ResponseUserProfileJson>(user);
    }
}
