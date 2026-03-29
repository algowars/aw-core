using ApplicationCore.Domain.Account;

namespace ApplicationCore.Interfaces.Repositories;

public interface IAccountRepository
{
    Task CreateAccountAsync(AccountModel account, CancellationToken cancellationToken);
    
    Task<AccountModel?> GetBySubAsync(string sub, CancellationToken cancellationToken);

    Task<AccountModel?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
}