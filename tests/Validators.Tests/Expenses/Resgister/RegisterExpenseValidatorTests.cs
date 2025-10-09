using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CommonTestUtilities.requests;

namespace Validators.Tests.Expenses.Resgister;
public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        //Arrange - parte de configurar as instancias
        var validator = new ResgisterExpenseValidator();
        //classe é static ent não precisa instanciar
        var request = RequestResgisterExpenseJsonBuilder.Build();

        //Act validar a ação
        var result = validator.Validate(request);

        //Assesrt 
        Assert.True(result.IsValid);   
    }

}
