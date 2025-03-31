using MediatR;
using Microsoft.AspNetCore.Mvc;
using Api.Impl.Cqrs;
using Schema;
using Base;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IMediator mediator;

    public AccountsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("GetAll")]
    public async Task<ApiResponse<List<AccountResponse>>> GetAll()
    {
        var result = await mediator.Send(new GetAllAccountsQuery());
        return result;
    }

    [HttpGet("GetById/{id}")]
    public async Task<ApiResponse<AccountResponse>> GetById(int id)
    {
        var result = await mediator.Send(new GetAccountByIdQuery(id));
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<AccountResponse>> Post([FromBody] AccountRequest request)
    {
        var result = await mediator.Send(new CreateAccountCommand(request));
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] AccountRequest request)
    {
        var result = await mediator.Send(new UpdateAccountCommand(id, request));
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var result = await mediator.Send(new DeleteAccountCommand(id));
        return result;
    }
}
