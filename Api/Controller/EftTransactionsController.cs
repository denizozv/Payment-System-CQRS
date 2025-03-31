using MediatR;
using Microsoft.AspNetCore.Mvc;
using Api.Impl.Cqrs;
using Schema;
using Base;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EftTransactionsController : ControllerBase
{
    private readonly IMediator mediator;

    public EftTransactionsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("GetAll")]
    public async Task<ApiResponse<List<EftTransactionResponse>>> GetAll()
    {
        var result = await mediator.Send(new GetAllEftTransactionsQuery());
        return result;
    }

    [HttpGet("GetById/{id}")]
    public async Task<ApiResponse<EftTransactionResponse>> GetById(int id)
    {
        var result = await mediator.Send(new GetEftTransactionByIdQuery(id));
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<EftTransactionResponse>> Post([FromBody] EftTransactionRequest request)
    {
        var result = await mediator.Send(new CreateEftTransactionCommand(request));
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] EftTransactionRequest request)
    {
        var result = await mediator.Send(new UpdateEftTransactionCommand(id, request));
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var result = await mediator.Send(new DeleteEftTransactionCommand(id));
        return result;
    }
}
