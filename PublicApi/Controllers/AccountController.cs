using ApplicationCore.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PublicApi.Dtos;

namespace PublicApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AccountController(IAccountAppService accountAppService) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAccountDto createAccountDto, CancellationToken cancellationToken)
    {
        var created = accountAppService.GetBySubAsync("test", cancellationToken);
        return Created(string.Empty, created);
    }
}