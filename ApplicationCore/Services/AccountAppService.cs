using ApplicationCore.Commands.Account.CreateAccount;
using ApplicationCore.Dtos;
using ApplicationCore.Queries.Account.GetAccountBySub;
using Ardalis.Result;
using MediatR;

namespace ApplicationCore.Services;

public interface IAccountAppService
{
    Task<Result<Guid>> CreateAccountAsync(
        string username,
        string sub,
        string? imageUrl,
        CancellationToken cancellationToken
    );

    Task<Result<AccountDto>> GetBySubAsync(string sub, CancellationToken cancellationToken);
}

public sealed class AccountAppService(IMediator mediator) : IAccountAppService
{
    public async Task<Result<Guid>> CreateAccountAsync(
        string username,
        string sub,
        string? imageUrl,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(
            new CreateAccountCommand(username, sub, imageUrl),
            cancellationToken
        );

        return result;
    }

    public async Task<Result<AccountDto>> GetBySubAsync(
        string sub,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(new GetAccountBySubQuery(sub), cancellationToken);

        return result.Map(source => new AccountDto(source.Id, source.Username, source.CreatedOn));
    }
}
