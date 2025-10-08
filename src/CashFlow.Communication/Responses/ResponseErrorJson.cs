namespace CashFlow.Communication.Responses;
public class ResponseErrorJson
{
    public List<string> ErrorMessages { get; set; }

    //passa uma mensagem de erro
    //recebe uma string e tem que transformar em uma lista
    public ResponseErrorJson(string errorMessage)
    {
        ErrorMessages = new List<string> { errorMessage };
    }
    //passa uma lista de erros
    public ResponseErrorJson(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
