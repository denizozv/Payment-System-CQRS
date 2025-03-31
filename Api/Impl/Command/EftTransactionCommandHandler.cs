using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;

namespace Api.Impl.Command;

public class EftTransactionCommandHandler :
    IRequestHandler<CreateEftTransactionCommand, ApiResponse<EftTransactionResponse>>,
    IRequestHandler<UpdateEftTransactionCommand, ApiResponse>,
    IRequestHandler<DeleteEftTransactionCommand, ApiResponse>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    public EftTransactionCommandHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<EftTransactionResponse>> Handle(CreateEftTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<EftTransaction>(request.model);
        entity.InsertedDate = DateTime.UtcNow;
        entity.InsertedUser = "admin";
        entity.IsActive = true;

        await dbContext.Set<EftTransaction>().AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<EftTransactionResponse>(entity);
        return new ApiResponse<EftTransactionResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateEftTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<EftTransaction>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
            return new ApiResponse("EFT transaction not found.");

        mapper.Map(request.model, entity);
        entity.UpdatedDate = DateTime.UtcNow;
        entity.UpdatedUser = "admin";

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteEftTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<EftTransaction>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
            return new ApiResponse("EFT transaction not found.");

        entity.IsActive = false;
        entity.UpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
