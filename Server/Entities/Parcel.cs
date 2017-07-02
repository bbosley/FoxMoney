using System;
using System.ComponentModel.DataAnnotations;

namespace FoxMoney.Server.Entities {
    public class Parcel : BaseEntity {
        public int HoldingId { get; set; }
        public Holding Holding { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int UnitsPurchased { get; set; }

        [DataType(DataType.Currency)]
        public decimal PurchasePrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal Brokerage { get; set; }

        public int UnitsSold { get; set; }

        public bool ParcelExhausted { get; set; }

        [DataType(DataType.Currency)]
        public decimal ParcelGain { get; set; }
    }
}