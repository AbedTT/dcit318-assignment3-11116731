using System;

namespace finaceMS
{
    public class DepositProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Processing Deposit of GHS {transaction.Amount:N2} into account.");
        }
    }
}