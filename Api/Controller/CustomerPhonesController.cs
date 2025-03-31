using MediatR;
using Microsoft.AspNetCore.Mvc;
using Api.Impl.Cqrs;
using Schema;
using Base;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerPhonesController : ControllerBase
{
    private readonly IMediator mediator;

    public CustomerPhonesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("GetAll")]
    public async Task<ApiResponse<List<CustomerPhoneResponse>>> GetAll()
    {
        var result = await mediator.Send(new GetAllCustomerPhonesQuery());
        return result;
    }

    [HttpGet("GetById/{id}")]
    public async Task<ApiResponse<CustomerPhoneResponse>> GetById(int id)
    {
        var result = await mediator.Send(new GetCustomerPhoneByIdQuery(id));
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<CustomerPhoneResponse>> Post([FromBody] CustomerPhoneRequest request)
    {
        var result = await mediator.Send(new CreateCustomerPhoneCommand(request));
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] CustomerPhoneRequest request)
    {
        var result = await mediator.Send(new UpdateCustomerPhoneCommand(id, request));
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var result = await mediator.Send(new DeleteCustomerPhoneCommand(id));
        return result;
    }
}
