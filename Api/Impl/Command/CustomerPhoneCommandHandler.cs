using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;

namespace Api.Impl.Command;

public class CustomerPhoneCommandHandler :
    IRequestHandler<CreateCustomerPhoneCommand, ApiResponse<CustomerPhoneResponse>>,
    IRequestHandler<UpdateCustomerPhoneCommand, ApiResponse>,
    IRequestHandler<DeleteCustomerPhoneCommand, ApiResponse>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    public CustomerPhoneCommandHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerPhoneResponse>> Handle(CreateCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<CustomerPhone>(request.model);
        entity.InsertedDate = DateTime.UtcNow;
        entity.InsertedUser = "admin";
        entity.IsActive = true;

        var result = await dbContext.Set<CustomerPhone>().AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<CustomerPhoneResponse>(result.Entity);
        return new ApiResponse<CustomerPhoneResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<CustomerPhone>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null) return new ApiResponse("Phone not found.");
        if (!entity.IsActive) return new ApiResponse("Phone is not active.");

        mapper.Map(request.model, entity);
        entity.UpdatedDate = DateTime.UtcNow;
        entity.UpdatedUser = "admin";

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<CustomerPhone>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null) return new ApiResponse("Phone not found.");

        entity.IsActive = false;
        entity.UpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
