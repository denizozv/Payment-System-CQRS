using Base;

namespace Schema;

public class AccountTransactionRequest : BaseRequest
{
    public long AccountId { get; set; }
    public string Description { get; set; }
    public decimal? DebitAmount { get; set; }
    public decimal? CreditAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
    public string? TransferType { get; set; }
}

public class AccountTransactionResponse : BaseResponse
{
    public long AccountId { get; set; }
    public string Description { get; set; }
    public decimal? DebitAmount { get; set; }
    public decimal? CreditAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
    public string? TransferType { get; set; }
}