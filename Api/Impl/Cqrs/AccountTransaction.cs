using MediatR;
using Base;
using Schema;

namespace Api.Impl.Cqrs;

public record GetAllAccountTransactionsQuery : IRequest<ApiResponse<List<AccountTransactionResponse>>>;
public record GetAccountTransactionByIdQuery(int Id) : IRequest<ApiResponse<AccountTransactionResponse>>;
public record CreateAccountTransactionCommand(AccountTransactionRequest model) : IRequest<ApiResponse<AccountTransactionResponse>>;
public record UpdateAccountTransactionCommand(int Id, AccountTransactionRequest model) : IRequest<ApiResponse>;
public record DeleteAccountTransactionCommand(int Id) : IRequest<ApiResponse>;
