using System;

namespace finaceMS
{
    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Processing Crypto Wallet transaction of GHS {transaction.Amount:N2} under category: {transaction.Category}");
        }
    }
}