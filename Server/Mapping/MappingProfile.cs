using AutoMapper;
using FoxMoney.Server.Entities;
using FoxMoney.Server.ViewModels;
using System.Linq;

namespace FoxMoney.Server.Mapping {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<Portfolio, PortfolioViewModel>()
                .ForMember(pr => pr.CapitalGain, opt => opt.ResolveUsing(p => p.CurrentRawValue - p.CostBase))
                .ForMember(pr => pr.Income, opt => opt.MapFrom(p => p.CurrentIncome))
                .ForMember(pr => pr.Return, opt => opt.ResolveUsing(p => (p.CurrentRawValue - p.CostBase) + p.CurrentIncome));

            CreateMap<Portfolio, PortfolioDetailViewModel>()
                 .IncludeBase<Portfolio, PortfolioViewModel>()
                 .ForMember(pr => pr.TotalValue, opt => opt.MapFrom(p => p.CurrentRawValue))
                 .ForMember(pr => pr.ReturnPercent, opt => opt.ResolveUsing(p => ((p.CurrentRawValue - p.CostBase) + p.CurrentIncome) / p.CostBase))
                 .ForMember(pr => pr.Holdings, opt => opt.MapFrom(p => p.Holdings.Where(h => h.HoldingClosed == false)));

            CreateMap<SavePortfolioViewModel, Portfolio>()
                .ForMember(sp => sp.Name, opt => opt.MapFrom(s => s.PortfolioName));

            CreateMap<Holding, HoldingViewModel>()
                .ForMember(hr => hr.Name, opt => opt.MapFrom(h => h.CustomName))
                .ForMember(hr => hr.LastPrice, opt => opt.MapFrom(h => (h == null ? 0 : h.SecurityPrice.ClosingPrice)))
                .ForMember(hr => hr.Quantity, opt => opt.MapFrom(h => h.TotalUnits))
                .ForMember(hr => hr.Value, opt => opt.ResolveUsing(h => h.TotalUnits * (h == null ? 0 : h.SecurityPrice.ClosingPrice)))
                .ForMember(hr => hr.CapitalGain, opt => opt.ResolveUsing(h => (h.TotalUnits * (h == null ? 0 : h.SecurityPrice.ClosingPrice)) - h.TotalCostBase))
                .ForMember(hr => hr.TotalReturn, opt => opt.ResolveUsing(h => ((h.TotalUnits * (h == null ? 0 : h.SecurityPrice.ClosingPrice)) - h.TotalCostBase) + h.TotalIncome))
                .ForMember(h => h.Income, opt => opt.MapFrom(h => h.TotalIncome))
                .ForMember(h => h.PortfolioId, opt => opt.MapFrom(h => h.PortfolioId));

            CreateMap<Holding, HoldingDetailViewModel>()
                .IncludeBase<Holding, HoldingViewModel>()
                .ForMember(hr => hr.ReturnPercent, opt => opt.ResolveUsing(h => (((h.TotalUnits * (h == null ? 0 : h.SecurityPrice.ClosingPrice)) - h.TotalCostBase) + h.TotalIncome) / h.TotalCostBase))
                .ForMember(hr => hr.UnitTransactions, opt => opt.MapFrom(h => h.Transactions))
                .ForMember(hr => hr.IncomeTransactions, opt => opt.MapFrom(h => h.Income));

            CreateMap<HoldingTransaction, HoldingTransactionViewModel>()
                .ForMember(htv => htv.Value, opt => opt.ResolveUsing(ht => (ht.Units * ht.UnitPrice) + ht.Brokerage));

            CreateMap<HoldingIncome, HoldingIncomeViewModel>();    
        }
    }
}