using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase
{
    public ResponseRegisteredExpenseJson Execute(RequestResgisterExpenseJson request)
    {
        //validações
        Validate(request);

        return new ResponseRegisteredExpenseJson();
    }

    private void Validate(RequestResgisterExpenseJson request)
    {
        var validator = new ResgisterExpenseValidator();
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
