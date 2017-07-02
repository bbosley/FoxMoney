namespace FoxMoney.Server.ViewModels {
    public class HoldingViewModel {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal LastPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public decimal CapitalGain { get; set; }
        public decimal Income { get; set; }
        public decimal CurrencyGain { get; set; }
        public decimal TotalReturn { get; set; }
    }
}