using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FoxMoney.Server.ViewModels {
    public class PortfolioDetailViewModel : PortfolioViewModel {
        public bool Draft { get; set; }
        public decimal TotalValue { get; set; }
        public decimal ReturnPercent { get; set; }
        public ICollection<HoldingViewModel> Holdings { get; set; }

        public PortfolioDetailViewModel() {
            Holdings = new Collection<HoldingViewModel>();
        }
    }
}