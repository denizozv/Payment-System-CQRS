using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;

namespace Api.Impl.Command;

public class AccountTransactionCommandHandler :
    IRequestHandler<CreateAccountTransactionCommand, ApiResponse<AccountTransactionResponse>>,
    IRequestHandler<UpdateAccountTransactionCommand, ApiResponse>,
    IRequestHandler<DeleteAccountTransactionCommand, ApiResponse>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    public AccountTransactionCommandHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AccountTransactionResponse>> Handle(CreateAccountTransactionCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<AccountTransaction>(request.model);
        mapped.InsertedDate = DateTime.UtcNow;
        mapped.InsertedUser = "system";
        mapped.IsActive = true;

        var entity = await dbContext.AddAsync(mapped, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<AccountTransactionResponse>(entity.Entity);
        return new ApiResponse<AccountTransactionResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateAccountTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<AccountTransaction>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null) return new ApiResponse("AccountTransaction not found");

        entity.Description = request.model.Description;
        entity.DebitAmount = request.model.DebitAmount;
        entity.CreditAmount = request.model.CreditAmount;
        entity.TransactionDate = request.model.TransactionDate;
        entity.TransferType = request.model.TransferType;
        entity.ReferenceNumber = request.model.ReferenceNumber;
        entity.UpdatedDate = DateTime.UtcNow;
        entity.UpdatedUser = "system";

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteAccountTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<AccountTransaction>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null) return new ApiResponse("AccountTransaction not found");

        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}