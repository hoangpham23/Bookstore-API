using Bookstore.Domain.Entites;

namespace Bookstore.Domain.Abstractions;

public interface IJwtTokenGenerator
{
    string GenerateToken(Customer customer);
}
