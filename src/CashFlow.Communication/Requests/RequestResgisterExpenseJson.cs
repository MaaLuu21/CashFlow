using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Requests;
public class RequestResgisterExpenseJson
{
    //request para registrar  despesa
    public string Title { get; set; } = string.Empty;
    public string Description  { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentsType PaymentType { get; set; }

}
