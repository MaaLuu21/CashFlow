namespace CashFlow.Application.UseCases.Users.GetProfile;
public interface IGetProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
