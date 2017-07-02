using System;

namespace FoxMoney.Server.ViewModels
{
    public class SavePortfolioViewModel
    {
        public string PortfolioName { get; set; }
        public int OwnerId { get; set; }
        public string YahooCode { get; set; }
        public int PurchaseAmount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal PurchaseFees { get; set; }
        public string CustomName { get; set; }
    }
}