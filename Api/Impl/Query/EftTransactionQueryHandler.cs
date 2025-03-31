using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;

namespace Api.Impl.Query;

public class EftTransactionQueryHandler :
    IRequestHandler<GetAllEftTransactionsQuery, ApiResponse<List<EftTransactionResponse>>>,
    IRequestHandler<GetEftTransactionByIdQuery, ApiResponse<EftTransactionResponse>>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    public EftTransactionQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<EftTransactionResponse>>> Handle(GetAllEftTransactionsQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<EftTransaction>().ToListAsync(cancellationToken);
        var mapped = mapper.Map<List<EftTransactionResponse>>(list);
        return new ApiResponse<List<EftTransactionResponse>>(mapped);
    }

    public async Task<ApiResponse<EftTransactionResponse>> Handle(GetEftTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<EftTransaction>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var mapped = mapper.Map<EftTransactionResponse>(entity);
        return new ApiResponse<EftTransactionResponse>(mapped);
    }
}
