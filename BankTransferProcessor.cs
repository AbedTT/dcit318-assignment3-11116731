// BankTransferProcessor.cs
using System;

public class BankTransferProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"Processing Bank Transfer for {transaction.Amount:C} from category: {transaction.Category}");
    }
}