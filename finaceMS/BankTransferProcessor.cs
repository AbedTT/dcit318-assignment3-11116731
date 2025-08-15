using System;

namespace finaceMS
{
    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Processing Bank Transfer for GHS {transaction.Amount:N2} from category: {transaction.Category}");
        }
    }
}