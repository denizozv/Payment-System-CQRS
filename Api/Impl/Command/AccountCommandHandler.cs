using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;

namespace Api.Impl.Query;

public class AccountCommandHandler :
    IRequestHandler<CreateAccountCommand, ApiResponse<AccountResponse>>,
    IRequestHandler<UpdateAccountCommand, ApiResponse>,
    IRequestHandler<DeleteAccountCommand, ApiResponse>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    public AccountCommandHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Account>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
            return new ApiResponse("Account not found");

        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Account>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
            return new ApiResponse("Account not found");

        mapper.Map(request.model, entity);
        entity.UpdatedDate = DateTime.UtcNow;
        entity.UpdatedUser = "admin";

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse<AccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<Account>(request.model);
        mapped.InsertedDate = DateTime.UtcNow;
        mapped.InsertedUser = "admin";
        mapped.IsActive = true;

        var entity = await dbContext.AddAsync(mapped, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        var response = mapper.Map<AccountResponse>(entity.Entity);
        return new ApiResponse<AccountResponse>(response);
    }
}
