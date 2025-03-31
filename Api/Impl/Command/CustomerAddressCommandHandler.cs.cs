using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;

namespace Api.Impl.Command;

public class CustomerAddressCommandHandler :
    IRequestHandler<CreateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>,
    IRequestHandler<UpdateCustomerAddressCommand, ApiResponse>,
    IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    public CustomerAddressCommandHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<CustomerAddress>(request.model);
        entity.InsertedDate = DateTime.UtcNow;
        entity.InsertedUser = "admin";
        entity.IsActive = true;

        var result = await dbContext.Set<CustomerAddress>().AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<CustomerAddressResponse>(result.Entity);
        return new ApiResponse<CustomerAddressResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<CustomerAddress>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
            return new ApiResponse("Customer address not found.");

        if (!entity.IsActive)
            return new ApiResponse("Customer address is not active.");

        mapper.Map(request.model, entity);
        entity.UpdatedDate = DateTime.UtcNow;
        entity.UpdatedUser = "admin";

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<CustomerAddress>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
            return new ApiResponse("Customer address not found.");

        entity.IsActive = false;
        entity.UpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
