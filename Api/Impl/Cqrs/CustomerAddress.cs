using MediatR;
using Base;
using Schema;

namespace Api.Impl.Cqrs;

public record GetAllCustomerAddressesQuery : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
public record GetCustomerAddressByIdQuery(int Id) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record CreateCustomerAddressCommand(CustomerAddressRequest model) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record UpdateCustomerAddressCommand(int Id, CustomerAddressRequest model) : IRequest<ApiResponse>;
public record DeleteCustomerAddressCommand(int Id) : IRequest<ApiResponse>;
