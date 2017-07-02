using System;

namespace FoxMoney.Quotes
{
    public class DataPoint
    {
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal AdjustedClose { get; set; }
        public int Volume { get; set; }
        public decimal DividendAmount { get; set; }
        public decimal SplitCoefficient { get; set; }
    }
}