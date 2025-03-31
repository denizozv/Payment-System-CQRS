using MediatR;
using Base;
using Schema;

namespace Api.Impl.Cqrs;

public record GetAllEftTransactionsQuery : IRequest<ApiResponse<List<EftTransactionResponse>>>;
public record GetEftTransactionByIdQuery(int Id) : IRequest<ApiResponse<EftTransactionResponse>>;
public record CreateEftTransactionCommand(EftTransactionRequest model) : IRequest<ApiResponse<EftTransactionResponse>>;
public record UpdateEftTransactionCommand(int Id, EftTransactionRequest model) : IRequest<ApiResponse>;
public record DeleteEftTransactionCommand(int Id) : IRequest<ApiResponse>;
