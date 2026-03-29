using ApplicationCore.Domain.Account;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Logging;
using Ardalis.Result;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Commands.Account.CreateAccount;

public sealed partial class CreateAccountHandler(
    IAccountRepository accountRepository,
    ILogger<CreateAccountHandler> logger,
    IValidator<CreateAccountCommand> validator
) : AbstractCommandHandler<CreateAccountCommand, Guid>(validator)
{
    protected override async Task<Result<Guid>> HandleValidated(
        CreateAccountCommand request,
        CancellationToken cancellationToken
    )
    {
        var id = Guid.NewGuid();

        var account = new AccountModel
        {
            Id = id,
            Username = request.Username,
            Sub = request.Sub,
            ImageUrl = request.ImageUrl,
            LastModifiedById = null,
        };

        await accountRepository.CreateAccountAsync(account, cancellationToken);

        LogCreated(logger, id, request.Sub);
        return Result.Success(id);
    }

    [LoggerMessage(
        EventId = LoggingEventIds.Accounts.Created,
        Level = LogLevel.Information,
        Message = "Created account {accountId} for sub {sub}"
    )]
    private static partial void LogCreated(ILogger logger, Guid accountId, string sub);

    [LoggerMessage(
        EventId = LoggingEventIds.Accounts.CreateFailed,
        Level = LogLevel.Error,
        Message = "Failed to create account for {username}/{sub}. DB message: {dbMessage}"
    )]
    private static partial void LogCreateFailed(
        ILogger logger,
        string username,
        string sub,
        string dbMessage
    );
}