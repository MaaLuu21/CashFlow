using CashFlow.Application.SharedValidtors;
using CashFlow.Communication.Requests;
using FluentValidation;
using FluentValidation.Validators;

namespace CashFlow.Application.UseCases.Users.ChangePassword;
public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(u => u.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
    }
}
