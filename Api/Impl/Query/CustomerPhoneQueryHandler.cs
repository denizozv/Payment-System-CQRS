using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;

namespace Api.Impl.Query;

public class CustomerPhoneQueryHandler :
    IRequestHandler<GetAllCustomerPhonesQuery, ApiResponse<List<CustomerPhoneResponse>>>,
    IRequestHandler<GetCustomerPhoneByIdQuery, ApiResponse<CustomerPhoneResponse>>
{
    private readonly AppDbContext context;
    private readonly IMapper mapper;

    public CustomerPhoneQueryHandler(AppDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetAllCustomerPhonesQuery request, CancellationToken cancellationToken)
    {
        var list = await context.Set<CustomerPhone>().ToListAsync(cancellationToken);
        var mapped = mapper.Map<List<CustomerPhoneResponse>>(list);
        return new ApiResponse<List<CustomerPhoneResponse>>(mapped);
    }

    public async Task<ApiResponse<CustomerPhoneResponse>> Handle(GetCustomerPhoneByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Set<CustomerPhone>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
            return new ApiResponse<CustomerPhoneResponse>("Customer phone not found.");

        var mapped = mapper.Map<CustomerPhoneResponse>(entity);
        return new ApiResponse<CustomerPhoneResponse>(mapped);
    }
}
