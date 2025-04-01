using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;
using Api.Exceptions;

namespace Api.Impl.Query;

public class CustomerQueryHandler :
    IRequestHandler<GetAllCustomersQuery, ApiResponse<List<CustomerResponse>>>,
    IRequestHandler<GetCustomerByIdQuery, ApiResponse<CustomerResponse>>
{
    private readonly AppDbContext context;
    private readonly IMapper mapper;

    public CustomerQueryHandler(AppDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await context.Set<Customer>()
                                     .Where(x => x.IsActive)
                                     .ToListAsync(cancellationToken);

        var mapped = mapper.Map<List<CustomerResponse>>(customers);
        return new ApiResponse<List<CustomerResponse>>(mapped);
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await context.Set<Customer>()
                                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive, cancellationToken);

        if (customer == null)
            throw new NotFoundException($"Customer with ID {request.Id} not found.");

        var mapped = mapper.Map<CustomerResponse>(customer);
        return new ApiResponse<CustomerResponse>(mapped);
    }
}
