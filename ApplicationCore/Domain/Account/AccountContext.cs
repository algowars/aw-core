namespace ApplicationCore.Domain.Account;

public interface IAccountContext
{
    AccountModel? Account { get; }
}

public sealed class AccountContext : IAccountContext
{
    public AccountModel? Account { get; private set; }
}