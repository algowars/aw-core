using ApplicationCore.Interfaces.Repositories;

namespace Infrastructure.Repositories;

internal class AccountRepository : IAccountRepository
{
    public Task<AccountModel> GetBySubAsync(string sub, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
