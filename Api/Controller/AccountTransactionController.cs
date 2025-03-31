using MediatR;
using Microsoft.AspNetCore.Mvc;
using Api.Impl.Cqrs;
using Schema;
using Base;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountTransactionsController : ControllerBase
{
    private readonly IMediator mediator;

    public AccountTransactionsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("GetAll")]
    public async Task<ApiResponse<List<AccountTransactionResponse>>> GetAll()
    {
        var result = await mediator.Send(new GetAllAccountTransactionsQuery());
        return result;
    }

    [HttpGet("GetById/{id}")]
    public async Task<ApiResponse<AccountTransactionResponse>> GetById(int id)
    {
        var result = await mediator.Send(new GetAccountTransactionByIdQuery(id));
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<AccountTransactionResponse>> Post([FromBody] AccountTransactionRequest request)
    {
        var result = await mediator.Send(new CreateAccountTransactionCommand(request));
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] AccountTransactionRequest request)
    {
        var result = await mediator.Send(new UpdateAccountTransactionCommand(id, request));
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var result = await mediator.Send(new DeleteAccountTransactionCommand(id));
        return result;
    }
}
