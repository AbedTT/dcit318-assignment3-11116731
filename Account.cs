public class Account
{
    public string AccountNumber { get; }
    public decimal Balance { get; protected set; }

    public Account(string accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
    }

    public virtual void ApplyTransaction(Transaction transaction)
    {
        Balance -= transaction.Amount;
        Console.WriteLine($"Withdrawal applied. New balance for account {AccountNumber}: {Balance:C}");
    }

    public void ApplyDeposit(Transaction transaction)
    {
        Balance += transaction.Amount;
        Console.WriteLine($"Deposit applied. New balance for account {AccountNumber}: {Balance:C}");
    }
}