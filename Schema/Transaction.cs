using Base;

namespace Schema;

public class TransactionResponse
{
    public decimal Amount { get; set; }
    public decimal FeeAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
}