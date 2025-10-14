﻿using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.requests;
using FluentAssertions;

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
        //Assert.True(result.IsValid);   sem FluenteAssertion
        result.IsValid.Should().BeTrue();
    }
    [Theory]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        //Arrange - parte de configurar as instancias

        var validator = new ResgisterExpenseValidator();
        //classe é static ent não precisa instanciar
        var request = RequestResgisterExpenseJsonBuilder.Build();
        request.Title = title;
       // request.Amount = -1;// testar erro

        //Act validar a ação
        var result = validator.Validate(request);


        //Assesrt 

        result.IsValid.Should().BeFalse(); 
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED)); // testa se contem somente um erro
    }

    [Fact]
    public void Error_Date_Future()
    {
        //Arrange
        var validator = new ResgisterExpenseValidator();
        var request = RequestResgisterExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);//Adiciona um dia e retorna uma data futura


        //Act validar a ação
        var result = validator.Validate(request);


        //Assesrt 
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSES_CANNOT_BE_FOR_THE_FUTURE)); // testa se contem somente um erro
    }
    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        //Arrange
        var validator = new ResgisterExpenseValidator();
        var request = RequestResgisterExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentsType)700;//Tem que fazer um casting

        //Act validar a ação
        var result = validator.Validate(request);


        //Assesrt 
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID)); // testa se contem somente um erro
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    //Passa como parametro o Amount para testar mais de um valor, no caso precisa testar para 0 e -1...
    //Para isso tem que usar o Theory em vez do Fact, e o InlineData recebe os valores que serão passados
    public void Error_Amount_Invalid(decimal amount)
    {
        //Arrange
        var validator = new ResgisterExpenseValidator();
        var request = RequestResgisterExpenseJsonBuilder.Build();
        request.Amount = amount;

        //Act validar a ação
        var result = validator.Validate(request);


        //Assesrt 
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO)); // testa se contem somente um erro
    }
}
