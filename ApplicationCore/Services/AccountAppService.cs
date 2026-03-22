using ApplicationCore.Dtos;

namespace ApplicationCore.Services;

public interface IAccountAppService
{
    AccountDto GetBySubAsync(string sub, CancellationToken cancellationToken);
}

public sealed class AccountAppService : IAccountAppService
{
    public AccountDto GetBySubAsync(string sub, CancellationToken cancellationToken)
    {
       return new AccountDto(Guid.NewGuid(), sub, new DateTime());
    }
}