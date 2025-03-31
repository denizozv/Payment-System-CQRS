using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;

namespace Api.Impl.Query;

public class MoneyTransferQueryHandler :
    IRequestHandler<GetAllMoneyTransfersQuery, ApiResponse<List<MoneyTransferResponse>>>,
    IRequestHandler<GetMoneyTransferByIdQuery, ApiResponse<MoneyTransferResponse>>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    public MoneyTransferQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<MoneyTransferResponse>>> Handle(GetAllMoneyTransfersQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<MoneyTransfer>().ToListAsync(cancellationToken);
        var mapped = mapper.Map<List<MoneyTransferResponse>>(list);
        return new ApiResponse<List<MoneyTransferResponse>>(mapped);
    }

    public async Task<ApiResponse<MoneyTransferResponse>> Handle(GetMoneyTransferByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<MoneyTransfer>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var mapped = mapper.Map<MoneyTransferResponse>(entity);
        return new ApiResponse<MoneyTransferResponse>(mapped);
    }
}
