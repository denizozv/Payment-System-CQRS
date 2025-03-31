using MediatR;
using Base;
using Schema;

namespace Api.Impl.Cqrs;

public record GetAllMoneyTransfersQuery : IRequest<ApiResponse<List<MoneyTransferResponse>>>;
public record GetMoneyTransferByIdQuery(int Id) : IRequest<ApiResponse<MoneyTransferResponse>>;
public record CreateMoneyTransferCommand(MoneyTransferRequest model) : IRequest<ApiResponse<MoneyTransferResponse>>;
public record UpdateMoneyTransferCommand(int Id, MoneyTransferRequest model) : IRequest<ApiResponse>;
public record DeleteMoneyTransferCommand(int Id) : IRequest<ApiResponse>;
