using ApplicationCore.Dtos;

namespace ApplicationCore.Queries.Account.GetAccountBySub;

public sealed record GetAccountBySubQuery(string Sub) : IQuery<AccountDto>;
