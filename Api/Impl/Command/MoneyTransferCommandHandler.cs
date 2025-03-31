using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;

namespace Api.Impl.Command;

public class MoneyTransferCommandHandler :
    IRequestHandler<CreateMoneyTransferCommand, ApiResponse<MoneyTransferResponse>>,
    IRequestHandler<UpdateMoneyTransferCommand, ApiResponse>,
    IRequestHandler<DeleteMoneyTransferCommand, ApiResponse>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    public MoneyTransferCommandHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<MoneyTransferResponse>> Handle(CreateMoneyTransferCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<MoneyTransfer>(request.model);
        entity.InsertedDate = DateTime.UtcNow;
        entity.InsertedUser = "admin";
        entity.IsActive = true;

        await dbContext.Set<MoneyTransfer>().AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<MoneyTransferResponse>(entity);
        return new ApiResponse<MoneyTransferResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateMoneyTransferCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<MoneyTransfer>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null) return new ApiResponse("Money transfer not found.");

        mapper.Map(request.model, entity);
        entity.UpdatedDate = DateTime.UtcNow;
        entity.UpdatedUser = "admin";

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteMoneyTransferCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<MoneyTransfer>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null) return new ApiResponse("Money transfer not found.");

        entity.IsActive = false;
        entity.UpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
