using ApplicationCore.Domain.Account;
using ApplicationCore.Dtos;
using ApplicationCore.Interfaces.Repositories;
using Ardalis.Result;

namespace ApplicationCore.Queries.Account.GetAccountBySub;

public sealed class GetAccountBySubHandler(IAccountRepository accountRepository)
    : IQueryHandler<GetAccountBySubQuery, AccountModel>
{
    public async Task<Result<AccountModel>> Handle(
        GetAccountBySubQuery request,
        CancellationToken cancellationToken
    )
    {
        var account = await accountRepository.GetBySubAsync(request.Sub, cancellationToken);
        return Result.Success(account);
    }
}