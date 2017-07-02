using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FoxMoney.Server.ViewModels {
    public class HoldingDetailViewModel : HoldingViewModel {
        public decimal ReturnPercent { get; set; }
        public ICollection<HoldingTransactionViewModel> UnitTransactions { get; set; }
        public ICollection<HoldingIncomeViewModel> IncomeTransactions { get; set; }

        public HoldingDetailViewModel() {
            UnitTransactions = new Collection<HoldingTransactionViewModel>();
            IncomeTransactions = new Collection<HoldingIncomeViewModel>();
        }
    }
}