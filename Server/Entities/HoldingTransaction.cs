using System;
using System.ComponentModel.DataAnnotations;

namespace FoxMoney.Server.Entities {
    public class HoldingTransaction : BaseEntity {
        public int HoldingId { get; set; }
        public Holding Holding { get; set; }

        public DateTime TransactionDate { get; set; }

        public HoldingTransactionType TransactionType { get; set; }

        public int Units { get; set; }

        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal Brokerage { get; set; }

        public Int64 GeneratedParcelId { get; set; }
        public Parcel GeneratedParcel { get; set; }
    }
}