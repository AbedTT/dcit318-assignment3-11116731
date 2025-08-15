using System;

namespace finaceMS
{
    public record Transaction(int Id, DateTime Date, decimal Amount, string Category);
}