using System;

namespace FoxMoney.Server.ViewModels
{
    public class HoldingTransactionViewModel
    {
        public long Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public int Units { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Brokerage { get; set; }
        public decimal Value { get; set; }
    }
}