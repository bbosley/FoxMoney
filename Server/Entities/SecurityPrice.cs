using System;
using System.ComponentModel.DataAnnotations;

namespace FoxMoney.Server.Entities {
    public class SecurityPrice : BaseEntity {
        public int SecurityId { get; set; }

        public Security Security { get; set; }

        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        public decimal ClosingPrice { get; set; }
    }
}