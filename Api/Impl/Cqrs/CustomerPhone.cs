using MediatR;
using Base;
using Schema;

namespace Api.Impl.Cqrs;

public record GetAllCustomerPhonesQuery : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
public record GetCustomerPhoneByIdQuery(int Id) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record CreateCustomerPhoneCommand(CustomerPhoneRequest model) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record UpdateCustomerPhoneCommand(int Id, CustomerPhoneRequest model) : IRequest<ApiResponse>;
public record DeleteCustomerPhoneCommand(int Id) : IRequest<ApiResponse>;
