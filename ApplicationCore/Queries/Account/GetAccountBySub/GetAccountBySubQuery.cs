namespace ApplicationCore.Queries.Account.GetAccountBySub;

public sealed record GetAccountBySubQuery(string Sub) : IQuery<AccountModel>;
