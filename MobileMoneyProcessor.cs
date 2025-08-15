using System;

public class MobileMoneyProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"Processing Mobile Money payment of {transaction.Amount:C} for category: {transaction.Category}");
    }
}