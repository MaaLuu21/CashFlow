namespace CashFlow.Communication.Responses;
public class ResponseErrorJson
{
    public string ErrorMessage { get; set; } = string.Empty;

    //O contrutor obriga que ResponseErrorJson passe um valor
    public ResponseErrorJson(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
