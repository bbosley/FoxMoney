using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoxMoney.Server.Entities {
    public class Holding : BaseEntity {
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

        public int SecurityId { get; set; }
        public Security Security { get; set; }

        public string CustomName { get; set; }

        public bool HoldingClosed { get; set; }

        public int TotalUnits { get; set; }
        
        public int TotalUnitsSold { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalCostBase { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalIncome { get; set; }

        [DataType(DataType.Currency)]
        public decimal RealisedGain { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalSoldCostBase { get; set; }

        public List<Parcel> Parcels { get; set; }

        public SecurityPrice SecurityPrice { get; set; }

        public List<HoldingTransaction> Transactions { get; set; }

        public List<HoldingIncome> Income { get; set; }
    }
}