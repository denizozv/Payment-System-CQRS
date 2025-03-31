using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Api.Impl.Cqrs;
using Base;
using Schema;

namespace Api.Impl.Query;

public class AccountQueryHandler :
    IRequestHandler<GetAllAccountsQuery, ApiResponse<List<AccountResponse>>>,
    IRequestHandler<GetAccountByIdQuery, ApiResponse<AccountResponse>>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    public AccountQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await dbContext.Set<Account>().ToListAsync(cancellationToken);
        var mapped = mapper.Map<List<AccountResponse>>(accounts);
        return new ApiResponse<List<AccountResponse>>(mapped);
    }

    public async Task<ApiResponse<AccountResponse>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await dbContext.Set<Account>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var mapped = mapper.Map<AccountResponse>(account);
        return new ApiResponse<AccountResponse>(mapped);
    }
}
