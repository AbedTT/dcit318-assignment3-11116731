namespace finaceMS
{
    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }
}