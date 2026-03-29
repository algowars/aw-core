using System.Security.Claims;
using ApplicationCore.Domain.Account;
using ApplicationCore.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PublicApi; // Ensure the namespace for the attributes
using PublicApi.Attributes;
using PublicApi.Dtos;

namespace PublicApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AccountController(IAccountAppService accountAppService, IAccountContext accountContext)
    : ControllerBase
{
    [HttpGet]
    [UserRateLimit(30, 60), GlobalRateLimit(300, 60)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [RequiresAccount]
    public IActionResult GetAccountBySub()
    {
        var account = accountContext.Account;
        if (account == null)
        {
            return NotFound();
        }

        return Ok(account);
    }

    [HttpPost]
    [UserRateLimit(3, 60), GlobalRateLimit(50, 60)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAccountAsync(
        [FromBody] CreateAccountDto createAccountDto,
        CancellationToken cancellationToken
    )
    {
        string? sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (sub is null)
        {
            return BadRequest();
        }

        var created = await accountAppService.CreateAccountAsync(
            createAccountDto.Username,
            sub,
            createAccountDto.ImageUrl,
            cancellationToken
        );
        return Created(string.Empty, created);
    }
}
