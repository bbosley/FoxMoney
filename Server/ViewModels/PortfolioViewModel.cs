namespace FoxMoney.Server.ViewModels {
    public class PortfolioViewModel {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Return { get; set; }
        public decimal CapitalGain { get; set; }
        public decimal Income { get; set; }
    }
}