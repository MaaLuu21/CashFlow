using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.requests;
//contrutor de uma requestResgisterExpenseJson
public class RequestResgisterExpenseJsonBuilder
{
    public static RequestResgisterExpenseJson Build()
    {
        //usando o pacote bogus
        //Pode se usar tanto assim

        /*var faker = new Faker();
        var request = new RequestResgisterExpenseJson
        {
            Title = faker.Commerce.Product(),
            Date = faker.Date.Past(),
        }; 
        */
        //quanto assim
        return new Faker<RequestResgisterExpenseJson>()
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentsType>())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000));
    }
}
