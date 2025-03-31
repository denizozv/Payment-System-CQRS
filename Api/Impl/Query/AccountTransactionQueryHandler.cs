using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;

namespace Api.Impl.Query;

public class AccountTransactionQueryHandler :
    IRequestHandler<GetAllAccountTransactionsQuery, ApiResponse<List<AccountTransactionResponse>>>,
    IRequestHandler<GetAccountTransactionByIdQuery, ApiResponse<AccountTransactionResponse>>
{
    private readonly AppDbContext context;
    private readonly IMapper mapper;

    public AccountTransactionQueryHandler(AppDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetAllAccountTransactionsQuery request, CancellationToken cancellationToken)
    {
        var list = await context.Set<AccountTransaction>().ToListAsync(cancellationToken);
        var mapped = mapper.Map<List<AccountTransactionResponse>>(list);
        return new ApiResponse<List<AccountTransactionResponse>>(mapped);
    }

    public async Task<ApiResponse<AccountTransactionResponse>> Handle(GetAccountTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Set<AccountTransaction>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
            return new ApiResponse<AccountTransactionResponse>("Transaction not found.");

        var mapped = mapper.Map<AccountTransactionResponse>(entity);
        return new ApiResponse<AccountTransactionResponse>(mapped);
    }
}
