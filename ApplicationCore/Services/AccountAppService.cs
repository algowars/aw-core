using ApplicationCore.Dtos;
using ApplicationCore.Queries.Account.GetAccountBySub;
using Ardalis.Result;
using MediatR;

namespace ApplicationCore.Services;

public interface IAccountAppService
{
    Task<Result<AccountDto>> GetBySubAsync(string sub, CancellationToken cancellationToken);
}

public sealed class AccountAppService(IMediator mediator) : IAccountAppService
{
    public async Task<Result<AccountDto>> GetBySubAsync(
        string sub,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(new GetAccountBySubQuery(sub), cancellationToken);

        return result.Map(source => new AccountDto(source.Id, source.Username, source.CreatedOn));
    }
}