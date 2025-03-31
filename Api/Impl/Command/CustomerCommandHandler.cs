using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;

namespace Api.Impl.Query;

public class CustomerCommandHandler :
IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponse>>,
IRequestHandler<UpdateCustomerCommand, ApiResponse>,
IRequestHandler<DeleteCustomerCommand, ApiResponse>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    public CustomerCommandHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
            return new ApiResponse("Customer not found");

        if (!entity.IsActive)
            return new ApiResponse("Customer is not active");

        entity.IsActive = false;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }


    public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
            return new ApiResponse("Customer not found");

        if (!entity.IsActive)
            return new ApiResponse("Customer is not active");

        entity.FirstName = request.customer.FirstName;
        entity.MiddleName = request.customer.MiddleName;
        entity.LastName = request.customer.LastName;
        entity.Email = request.customer.Email;
        entity.UpdatedDate = DateTime.Now;
        entity.UpdatedUser = null;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<Customer>(request.customer);
        mapped.InsertedDate = DateTime.Now;
        mapped.OpenDate = DateTime.Now;
        mapped.InsertedUser = "test";
        mapped.CustomerNumber = new Random().Next(1000000, 999999999);
        mapped.IsActive = true;

        var entity = await dbContext.AddAsync(mapped, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        var response = mapper.Map<CustomerResponse>(entity.Entity);
        return new ApiResponse<CustomerResponse>(response);
    }
}