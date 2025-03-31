using MediatR;
using Microsoft.AspNetCore.Mvc;
using Api.Impl.Cqrs;
using Schema;
using Base;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoneyTransfersController : ControllerBase
{
    private readonly IMediator mediator;

    public MoneyTransfersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("GetAll")]
    public async Task<ApiResponse<List<MoneyTransferResponse>>> GetAll()
    {
        var result = await mediator.Send(new GetAllMoneyTransfersQuery());
        return result;
    }

    [HttpGet("GetById/{id}")]
    public async Task<ApiResponse<MoneyTransferResponse>> GetById(int id)
    {
        var result = await mediator.Send(new GetMoneyTransferByIdQuery(id));
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<MoneyTransferResponse>> Post([FromBody] MoneyTransferRequest request)
    {
        var result = await mediator.Send(new CreateMoneyTransferCommand(request));
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] MoneyTransferRequest request)
    {
        var result = await mediator.Send(new UpdateMoneyTransferCommand(id, request));
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var result = await mediator.Send(new DeleteMoneyTransferCommand(id));
        return result;
    }
}
