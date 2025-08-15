using System;

public class DepositProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"Processing Deposit of {transaction.Amount:C} into account.");
    }
}