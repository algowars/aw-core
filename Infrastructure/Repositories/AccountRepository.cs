using ApplicationCore.Domain.Account;
using ApplicationCore.Interfaces.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Entities.Account;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class AccountRepository(AppDbContext dbContext) : IAccountRepository
{
    public async Task CreateAccountAsync(AccountModel account, CancellationToken cancellationToken)
    {
        var entity = account.Adapt<AccountEntity>();

        await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<AccountModel?> GetBySubAsync(string sub, CancellationToken cancellationToken)
    {
        return await dbContext.Accounts.Where(a => a.Sub == sub).ProjectToType<AccountModel>()
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<AccountModel?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await dbContext.Accounts.Where(a => a.Username == username).ProjectToType<AccountModel>().SingleOrDefaultAsync(cancellationToken);
    }
}