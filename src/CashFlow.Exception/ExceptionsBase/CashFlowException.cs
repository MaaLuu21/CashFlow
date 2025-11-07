namespace CashFlow.Exception.ExceptionsBase;
//abstract para bloquear o acesso direto a classe
public abstract class CashFlowException : SystemException
{
    protected CashFlowException(string message) : base(message)
    {
        
    }
    //para seguir o principio O, aberto a extensão e fechado a modificação
    public abstract int StatusCode { get; }
    //forçar todas as classes que extenderem de cashFlowException a implementar um GetErrors
    public abstract List<string> GetErros();
}
