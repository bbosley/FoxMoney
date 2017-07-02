using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using FoxMoney.Server.Entities;
using FoxMoney.Server.Repositories.Abstract;
using FoxMoney.Server.Services;
using FoxMoney.Server.ViewModels;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FoxMoney.Server.Repositories;
using System.Collections.Generic;

namespace FoxMoney.Server.Controllers.api
{

    [Authorize]
    [Route("api/[controller]")]
    public class HoldingController : BaseController
    {

        private readonly IMapper mapper;
        private readonly IEntityBaseRepository<Holding> repository;
        private readonly ParcelRepository parcelRepository;

        public HoldingController(IMapper mapper,
            IEntityBaseRepository<Holding> repository,
            ParcelRepository parcelRepository)
        {
            this.parcelRepository = parcelRepository;
            this.repository = repository;

            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<HoldingDetailViewModel> GetHolding(int id)
        {
            //var holding = await repository.GetSingleAsync(id);
            var holding = repository.GetSingle(h => h.Id == id,
                h => h.Security,
                h => h.SecurityPrice,
                h => h.Transactions,
                h => h.Income);
            return mapper.Map<Holding, HoldingDetailViewModel>(holding);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateHolding([FromBody] SaveHoldingViewModel holdingViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            BackgroundJob.Enqueue<BackgroundProcessingService>(service =>
                service.ProcessNewHoldingComponents(holdingViewModel));

            var response = new TransactionIdentifierViewModel
            {
                Id = 1
            };

            return Ok(response);
        }

        [HttpPost("{id}/UnitTrans")]
        public async Task<IActionResult> CreateUnitTransaction(int id, [FromBody] SaveUnitTransViewModel unitTransViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var holding = await repository.GetSingleAsync(id);

            //Yes this is wrong
            var context = repository.GetContext();

            var realisedGain = 0.0M;

            var transaction = new HoldingTransaction
            {
                TransactionDate = unitTransViewModel.TransactionDate,
                TransactionType = (unitTransViewModel.TransactionType == "Buy") ? HoldingTransactionType.Buy : HoldingTransactionType.Sell,
                Units = unitTransViewModel.Units,
                UnitPrice = unitTransViewModel.UnitPrice,
                Brokerage = unitTransViewModel.Brokerage,
                Holding = holding
            };

            if (unitTransViewModel.TransactionType == "Buy")
            {
                var parcel = new Parcel
                {
                    PurchaseDate = unitTransViewModel.TransactionDate,
                    UnitsPurchased = unitTransViewModel.Units,
                    PurchasePrice = unitTransViewModel.UnitPrice,
                    Brokerage = unitTransViewModel.Brokerage,
                    UnitsSold = 0,
                    ParcelExhausted = false,
                    Holding = holding
                };
                context.Add<Parcel>(parcel);
                transaction.GeneratedParcel = parcel;
            }

            if (unitTransViewModel.TransactionType == "Sell")
            {
                var parcels = await parcelRepository.AvailableParcelsAsync(holding.Id);
                var unitsRemaining = unitTransViewModel.Units;
                var updatedParcels = new List<Parcel>();
                var unitisedSaleBrokerage = unitTransViewModel.Brokerage / unitTransViewModel.Units;

                foreach (Parcel parcel in parcels)
                {
                    var parcelUnits = parcel.UnitsPurchased - parcel.UnitsSold;
                    var originalCost = (parcelUnits * parcel.PurchasePrice) + ((parcel.Brokerage / parcel.UnitsPurchased) * parcelUnits);
                    var saleProceeds = (parcelUnits * unitTransViewModel.UnitPrice) - (parcelUnits * unitisedSaleBrokerage);
                    var parcelGain = saleProceeds - originalCost;

                    if (unitsRemaining >= parcelUnits) {
                        parcel.UnitsSold = parcel.UnitsPurchased;
                        parcel.ParcelExhausted = true;
                        unitsRemaining -= parcelUnits;
                    }
                    else
                    {
                        parcel.UnitsSold += unitsRemaining;
                        unitsRemaining = 0;    
                    }

                    parcel.ParcelGain = parcelGain;

                    updatedParcels.Add(parcel);
                    
                    if (unitsRemaining <= 0) break;
                }

                foreach (Parcel parcel in updatedParcels) {
                    parcelRepository.Edit(parcel);
                    parcelRepository.Commit();
                }

                //holding.RealisedGain += realisedGain;
                //holding.TotalUnitsSold += unitTransViewModel.Units;
                //context.Update<Holding>(holding);
                //await context.SaveChangesAsync();
            }

            context.Add<HoldingTransaction>(transaction);
            await context.SaveChangesAsync();

            if (unitTransViewModel.TransactionType == "Buy") {
                BackgroundJob.Enqueue<BackgroundProcessingService>(service =>
                    service.UpdatePortfolioTotals(holding.PortfolioId));
            } else {
                //BackgroundJob.Enqueue<BackgroundProcessingService>(service =>
                //    service.UpdatePortfolioPostSell(holding.PortfolioId, holding.Id, holding.RealisedGain + realisedGain, holding.TotalUnitsSold + unitTransViewModel.Units));
                BackgroundJob.Enqueue<BackgroundProcessingService>(service =>
                    service.UpdatePortfolioTotals(holding.PortfolioId));
            }

            var response = new TransactionIdentifierViewModel
            {
                Id = transaction.Id
            };

            return Ok(response);
        }

        [HttpPost("{id}/IncomeTrans")]
        public async Task<IActionResult> CreateIncomeTransaction(int id, [FromBody] SaveIncomeTransViewModel incomeTransViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var holding = await repository.GetSingleAsync(id);

            var context = repository.GetContext();

            var income = new HoldingIncome
            {
                IncomeDate = incomeTransViewModel.TransactionDate,
                Income = incomeTransViewModel.IncomeAmount,
                IncomeReinvested = incomeTransViewModel.IncomeReinvested,
                Holding = holding
            };

            if (incomeTransViewModel.IncomeReinvested)
            {
                var parcel = new Parcel
                {
                    PurchaseDate = incomeTransViewModel.TransactionDate,
                    UnitsPurchased = incomeTransViewModel.Units,
                    PurchasePrice = incomeTransViewModel.UnitPrice,
                    Brokerage = 0,
                    UnitsSold = 0,
                    ParcelExhausted = false,
                    Holding = holding
                };

                var transaction = new HoldingTransaction
                {
                    TransactionDate = incomeTransViewModel.TransactionDate,
                    TransactionType = HoldingTransactionType.Buy,
                    Units = incomeTransViewModel.Units,
                    UnitPrice = incomeTransViewModel.UnitPrice,
                    Brokerage = 0,
                    GeneratedParcel = parcel,
                    Holding = holding
                };

                income.ReinvestmentHoldingTransaction = transaction;

                context.Add<Parcel>(parcel);
                context.Add<HoldingTransaction>(transaction);
            }

            context.Add<HoldingIncome>(income);
            await context.SaveChangesAsync();

            BackgroundJob.Enqueue<BackgroundProcessingService>(service =>
                service.UpdatePortfolioTotals(holding.PortfolioId));

            var response = new TransactionIdentifierViewModel
            {
                Id = income.Id
            };

            return Ok(response);
        }
    }
}