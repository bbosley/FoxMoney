using System;

namespace FoxMoney.Server.ViewModels
{
    public class HoldingIncomeViewModel
    {
        public long Id { get; set; }
        public DateTime IncomeDate { get; set; }
        public decimal Income { get; set; }
        
    }
}