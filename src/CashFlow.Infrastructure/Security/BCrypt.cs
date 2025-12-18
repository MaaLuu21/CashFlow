using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt; // pq o nome da classe é a mesma da biblioteca

namespace CashFlow.Infrastructure.Security;
public class BCrypt : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }
}
