using MediatR;
using Base;
using Schema;

namespace Api.Impl.Cqrs;

public record GetAllAccountsQuery : IRequest<ApiResponse<List<AccountResponse>>>;
public record GetAccountByIdQuery(int Id) : IRequest<ApiResponse<AccountResponse>>;
public record CreateAccountCommand(AccountRequest model) : IRequest<ApiResponse<AccountResponse>>;
public record UpdateAccountCommand(int Id, AccountRequest model) : IRequest<ApiResponse>;
public record DeleteAccountCommand(int Id) : IRequest<ApiResponse>;
