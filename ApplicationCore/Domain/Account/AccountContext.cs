using ApplicationCore.Dtos;

namespace ApplicationCore.Domain.Account;

public interface IAccountContext
{
    AccountDto? Account { get; set; }
}

public sealed class AccountContext : IAccountContext
{
    public AccountDto? Account { get; set; }
}
