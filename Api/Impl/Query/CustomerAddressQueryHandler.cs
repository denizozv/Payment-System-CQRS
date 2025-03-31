using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;

namespace Api.Impl.Query;

public class CustomerAddressQueryHandler :
    IRequestHandler<GetAllCustomerAddressesQuery, ApiResponse<List<CustomerAddressResponse>>>,
    IRequestHandler<GetCustomerAddressByIdQuery, ApiResponse<CustomerAddressResponse>>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    public CustomerAddressQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetAllCustomerAddressesQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<CustomerAddress>().ToListAsync(cancellationToken);
        var mapped = mapper.Map<List<CustomerAddressResponse>>(list);
        return new ApiResponse<List<CustomerAddressResponse>>(mapped);
    }

    public async Task<ApiResponse<CustomerAddressResponse>> Handle(GetCustomerAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<CustomerAddress>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
            return new ApiResponse<CustomerAddressResponse>("Customer address not found.");

        var mapped = mapper.Map<CustomerAddressResponse>(entity);
        return new ApiResponse<CustomerAddressResponse>(mapped);
    }
}
