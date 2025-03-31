using MediatR;
using Microsoft.AspNetCore.Mvc;
using Api.Impl.Cqrs;
using Schema;
using Base;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerAddressesController : ControllerBase
{
    private readonly IMediator mediator;

    public CustomerAddressesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("GetAll")]
    public async Task<ApiResponse<List<CustomerAddressResponse>>> GetAll()
    {
        var result = await mediator.Send(new GetAllCustomerAddressesQuery());
        return result;
    }

    [HttpGet("GetById/{id}")]
    public async Task<ApiResponse<CustomerAddressResponse>> GetById(int id)
    {
        var result = await mediator.Send(new GetCustomerAddressByIdQuery(id));
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<CustomerAddressResponse>> Post([FromBody] CustomerAddressRequest request)
    {
        var result = await mediator.Send(new CreateCustomerAddressCommand(request));
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] CustomerAddressRequest request)
    {
        var result = await mediator.Send(new UpdateCustomerAddressCommand(id, request));
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var result = await mediator.Send(new DeleteCustomerAddressCommand(id));
        return result;
    }
}
