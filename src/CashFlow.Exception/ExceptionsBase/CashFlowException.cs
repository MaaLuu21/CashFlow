namespace CashFlow.Exception.ExceptionsBase;
//abstract para bloquear o acesso direto a classe
public abstract class CashFlowException : SystemException
{
    protected CashFlowException(string message) : base(message)
    {
        
    }
}
