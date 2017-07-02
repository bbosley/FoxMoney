using System;

namespace FoxMoney.Server.Entities {
    public class HoldingIncome : BaseEntity {
        public Int64 HoldingId { get; set; }
        public Holding Holding { get; set; }

        public DateTime IncomeDate { get; set; }

        public decimal Income { get; set; }

        public bool IncomeReinvested { get; set; }

        public HoldingTransaction ReinvestmentHoldingTransaction { get; set; }
    }
}