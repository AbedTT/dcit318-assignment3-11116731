using System;

public class CryptoWalletProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"Processing Crypto Wallet transaction of {transaction.Amount:C} under category: {transaction.Category}");
    }
}