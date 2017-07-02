using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoxMoney.Server.Entities {
    public class Portfolio : BaseEntity {
        public string Name { get; set; }

        public bool Active { get; set; }

        public bool Draft { get; set; }

        public int OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public List<Holding> Holdings { get; set; }

        [DataType(DataType.Currency)]
        public decimal CostBase { get; set; }

        [DataType(DataType.Currency)]
        public decimal CurrentRawValue { get; set; }

        [DataType(DataType.Currency)]
        public decimal CurrentIncome { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalRealisedGain { get; set; }

        [DataType(DataType.Currency)]
        public decimal SoldCostBase { get; set; }
    }
}