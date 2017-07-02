using System;

namespace FoxMoney.Server.ViewModels
{
    public class SaveHoldingViewModel
    {
        public string CustomName { get; set; }
        public int PortfolioId { get; set; }
        public string YahooCode { get; set; }
        public int PurchaseAmount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal PurchaseFees { get; set; }
    }
}