using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Dtos;
using Ardalis.Result;

namespace ApplicationCore.Queries.Account.GetAccountBySub;

public sealed class GetAccountBySubHandler() : IQueryHandler<GetAccountBySubQuery, AccountDto>
{
    public async Task<Result<AccountDto>> Handle(
        GetAccountBySubQuery request,
        CancellationToken cancellationToken
    ) { }
}
