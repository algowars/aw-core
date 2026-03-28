namespace ApplicationCore.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<AccountModel> GetBySubAsync(string sub, CancellationToken cancellationToken);
}
