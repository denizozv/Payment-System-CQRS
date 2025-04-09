using Microsoft.EntityFrameworkCore;
using Api.Domain;
using Base;

namespace Api.Impl.Service;

public class AccountService : IAccountService
{
    private readonly AppDbContext dbContext;
    public AccountService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ApiResponse> CreateIncomingAccountTransaction(long accountId, decimal amount, string description, string refNumber)
    {
        if (amount <= 0)
            return new ApiResponse("Amount must be greater than zero");
        if (string.IsNullOrEmpty(description))
            return new ApiResponse("Description cannot be empty");
        if (string.IsNullOrEmpty(refNumber))
            return new ApiResponse("Reference number cannot be empty");

        var account = await dbContext.Set<Account>().FirstOrDefaultAsync(x => x.Id == accountId);
        if (account == null)
            return new ApiResponse("Account not found");
        if (!account.IsActive)
            return new ApiResponse("Account is not active");

        var accountTransaction = new AccountTransaction
        {
            AccountId = accountId,
            DebitAmount = amount,
            Description = description,
            TransactionDate = DateTime.Now,
            InsertedDate = DateTime.Now,
            InsertedUser = "System",
            ReferenceNumber = refNumber,
        };

        await dbContext.Set<AccountTransaction>().AddAsync(accountTransaction);

        account.Balance += amount;
        account.UpdatedDate = DateTime.Now;
        account.UpdatedUser = null;

        await dbContext.SaveChangesAsync();
        return new ApiResponse();
    }

    public async Task<ApiResponse> CreateOutgoingAccountTransaction(long accountId, decimal amount, decimal feeAmount, string description, string refNumber)
    {
        if (amount <= 0)
            return new ApiResponse("Amount must be greater than zero");
        if (string.IsNullOrEmpty(description))
            return new ApiResponse("Description cannot be empty");
        if (string.IsNullOrEmpty(refNumber))
            return new ApiResponse("Reference number cannot be empty");

        var account = await dbContext.Set<Account>().FirstOrDefaultAsync(x => x.Id == accountId);
        if (account == null)
            return new ApiResponse("Account not found");
        if (!account.IsActive)
            return new ApiResponse("Account is not active");

        if (account.Balance < (amount + feeAmount))
            return new ApiResponse("Account balance is not enough");

        var accountTransaction = new AccountTransaction
        {
            AccountId = accountId,
            CreditAmount = amount,
            Description = description,
            TransactionDate = DateTime.Now,
            InsertedDate = DateTime.Now,
            InsertedUser = "System",
            ReferenceNumber = refNumber,
        };
        
        if (feeAmount > 0)
        {
            var accountTransactionFee = new AccountTransaction
            {
                AccountId = accountId,
                CreditAmount = feeAmount,
                Description = description + " - Fee",
                TransactionDate = DateTime.Now,
                InsertedDate = DateTime.Now,
                InsertedUser = "System",
                ReferenceNumber = refNumber,
                TransferType = "Fee",
            };
            await dbContext.Set<AccountTransaction>().AddAsync(accountTransactionFee);
        }
        await dbContext.Set<AccountTransaction>().AddAsync(accountTransaction);

        account.Balance -= (amount + feeAmount);
        account.UpdatedDate = DateTime.Now;
        account.UpdatedUser = null;

        await dbContext.SaveChangesAsync();
        return new ApiResponse();
    }
}
