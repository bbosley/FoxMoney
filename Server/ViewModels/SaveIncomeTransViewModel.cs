using System;

namespace FoxMoney.Server.ViewModels
{
    public class SaveIncomeTransViewModel
    {
        public DateTime TransactionDate { get; set; }
        public decimal IncomeAmount { get; set; }
        public bool IncomeReinvested { get; set; }
        public int Units { get; set; }
        public decimal UnitPrice { get; set; }
    }
}