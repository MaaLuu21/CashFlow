using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class ResgisterExpenseValidator : AbstractValidator<RequestResgisterExpenseJson>
{
    public ResgisterExpenseValidator()
    {
        //resource usado para evitar a duplicação de código
        //pode haver mais de uma classe que exiba uma exceção e ai para não duplicar usa o resource
        //são armazenadas mensagens de erro, textos ou valores reutilizáveis
        //— em vez de repetir o mesmo texto ou lógica em várias classes.
        RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
        RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.EXPENSES_CANNOT_BE_FOR_THE_FUTURE);
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENT_TYPE_INVALID);
    }
}
