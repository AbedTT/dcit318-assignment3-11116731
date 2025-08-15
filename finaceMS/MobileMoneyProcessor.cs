using System;

namespace finaceMS
{
    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Processing Mobile Money payment of GHS {transaction.Amount:N2} for category: {transaction.Category}");
        }
    }
}