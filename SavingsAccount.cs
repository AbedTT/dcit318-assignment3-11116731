using System;

public sealed class SavingsAccount : Account
{
    public SavingsAccount(string accountNumber, decimal initialBalance) : base(accountNumber, initialBalance) { }

    public override void ApplyTransaction(Transaction transaction)
    {
        if (transaction.Amount > Balance)
        {
            Console.WriteLine($"Insufficient funds. Transaction of {transaction.Amount:C} from category '{transaction.Category}' cannot be completed.");
        }
        else
        {
            base.ApplyTransaction(transaction);
        }
    }
}