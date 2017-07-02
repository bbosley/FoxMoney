using System;
using System.Linq;
using FoxMoney.Quotes;
using FoxMoney.Server.Entities;
using FoxMoney.Server.Repositories;
using FoxMoney.Server.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FoxMoney.Server.Services {
    public class BackgroundProcessingService : IDisposable {
        private readonly ApplicationDbContext context;
        private SecurityPriceRepository securityPriceRepository;
        private EntityBaseRepository<Security> securityRepository;
        private EntityBaseRepository<Portfolio> portfolioRepository;
        private EntityBaseRepository<Holding> holdingRepository;
        //private EntityBaseRepository<Parcel> parcelRepository;
        //private EntityBaseRepository<HoldingTransaction> holdingTransactionRepository;
        //private YahooFinanceClient yahooClient;
        private AlphaVantageClient avClient;

        public BackgroundProcessingService(IConfigurationRoot configuration) { 
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            dbContextOptionsBuilder.UseNpgsql(configuration["Data:PostgresqlConnectionString"]);
            context = new ApplicationDbContext(dbContextOptionsBuilder.Options);

            portfolioRepository = new EntityBaseRepository<Portfolio>(context);
            securityPriceRepository = new SecurityPriceRepository(context);
            securityRepository = new EntityBaseRepository<Security>(context);
            holdingRepository = new EntityBaseRepository<Holding>(context);
            //parcelRepository = new EntityBaseRepository<Parcel>(context);
            //holdingTransactionRepository = new EntityBaseRepository<HoldingTransaction>(context);

            //yahooClient = new YahooFinanceClient(configuration["Yahoo:Cookie"], configuration["Yahoo:Crumb"]);
            avClient = new AlphaVantageClient(configuration["AlphaVantage:ApiKey"]);
        }

        public void ProcessNewHoldingComponents(SaveHoldingViewModel components) {
            var portfolio = portfolioRepository.GetSingle(p => p.Id == components.PortfolioId);

            var newComps = new SavePortfolioViewModel{
                YahooCode = components.YahooCode,
                PurchaseAmount = components.PurchaseAmount,
                PurchaseDate = components.PurchaseDate,
                PurchasePrice = components.PurchasePrice,
                PurchaseFees = components.PurchaseFees,
                CustomName = components.CustomName
            };

            ProcessNewPortfolioComponents(portfolio, newComps);
        }

        public void ProcessNewPortfolioComponents(Portfolio portfolio, SavePortfolioViewModel components) {
            Security security = securityRepository.GetSingle(s => s.YahooCode == components.YahooCode);
            if (security == null){
                //var quote = yahooClient.GetRealTimeData(components.YahooCode);

                security = new Security() {
                    YahooCode = components.YahooCode,
                    Name = components.CustomName
                };
                //securityRepository.Add(security);
                context.Add<Security>(security);
            }

            //Save Holding
            Holding holding = new Holding()
            {
                Security = security,
                PortfolioId = portfolio.Id,
                CustomName = security.Name,
                TotalUnits = components.PurchaseAmount,
                TotalCostBase = (components.PurchaseAmount * components.PurchasePrice) + components.PurchaseFees
            };
            //holdingRepository.Add(holding);
            context.Add<Holding>(holding);

            //Save Parcel
            Parcel parcel = new Parcel()
            {
                Holding = holding,
                PurchaseDate = components.PurchaseDate,
                UnitsPurchased = components.PurchaseAmount,
                PurchasePrice = components.PurchasePrice,
                Brokerage = components.PurchaseFees
            };
            //parcelRepository.Add(parcel);
            context.Add<Parcel>(parcel);

            //Save Holding Transaction
            HoldingTransaction transaction = new HoldingTransaction()
            {
                Holding = holding,
                TransactionDate = components.PurchaseDate,
                TransactionType = HoldingTransactionType.Buy,
                Units = components.PurchaseAmount,
                UnitPrice = components.PurchasePrice,
                Brokerage = components.PurchaseFees,
                GeneratedParcel = parcel
            };
            //holdingTransactionRepository.Add(transaction);
            context.Add<HoldingTransaction>(transaction);

            context.SaveChanges();

            DownloadQuotes(security, parcel.PurchaseDate, true);
            UpdatePortfolioTotals(portfolio.Id);
        }

        public void ProcessNewHolding(Parcel parcel)
        {
            Holding holding = parcel.Holding;
            Security security = holding.Security;
            Portfolio portfolio = holding.Portfolio;
            DownloadQuotes(security, parcel.PurchaseDate, true);
            UpdatePortfolioTotals(portfolio.Id);
        }

        public void UpdatePortfolioPostSell(Int64 portfolioId, int holdingId, decimal realisedGain, int unitsSold) {
            Holding holding = holdingRepository.GetSingle(h => h.Id == holdingId);
            holding.RealisedGain = realisedGain;
            holding.TotalUnitsSold = unitsSold;
            context.Update<Holding>(holding);
            context.SaveChanges();

            UpdatePortfolioTotals(portfolioId);
        }

        public void UpdatePortfolioTotals(Int64 portfolioId)
        {
            decimal costBase = 0.0M;
            decimal rawValue = 0.0M;
            decimal totalIncome = 0.0M;
            decimal realisedGain = 0.0M;
            decimal soldCostBase = 0.0M;

            Portfolio portfolio = context.Portfolios.Include(p => p.Holdings)
                .ThenInclude(h => h.Parcels)
                .Include(p => p.Holdings)
                .ThenInclude(p => p.Security)
                .Include(p => p.Holdings)
                .ThenInclude(h => h.Income)
                .Single(p => p.Id == portfolioId);

            context.Entry(portfolio).Reload();

            foreach (Holding holding in portfolio.Holdings.Where(h => h.HoldingClosed == false))
            {
                decimal holdingCostBase = 0.0M;
                int holdingUnits = 0;
                decimal holdingIncome = 0.0M;
                decimal holdingSoldCostBase = 0.0M;
                decimal holdingRealisedGain = 0.0M;
                int holdingUnitsSold = 0;

                foreach (Parcel parcel in holding.Parcels)
                {
                    var parcelRemainingUnits = parcel.UnitsPurchased - parcel.UnitsSold;
                    var parcelUnitBrokerage = parcel.Brokerage / parcel.UnitsPurchased;
                    holdingUnits += parcelRemainingUnits;
                    holdingCostBase += (parcelRemainingUnits * parcel.PurchasePrice) + (parcelUnitBrokerage * parcelRemainingUnits);
                    holdingSoldCostBase += (parcel.UnitsSold * parcel.PurchasePrice) + (parcel.UnitsSold * parcelUnitBrokerage);
                    holdingRealisedGain += parcel.ParcelGain;
                    holdingUnitsSold += parcel.UnitsSold;
                }

                holding.TotalCostBase = holdingCostBase;
                holding.TotalUnits = holdingUnits;
                holding.TotalUnitsSold = holdingUnitsSold;
                holding.RealisedGain = holdingRealisedGain;
                holding.TotalSoldCostBase = holdingSoldCostBase;

                if (holding.TotalUnits <=  0) {
                    holding.HoldingClosed = true;
                }

                costBase += holdingCostBase;
                soldCostBase += holdingSoldCostBase;

                SecurityPrice latestPrice = securityPriceRepository.GetLatestSecurityPrice(holding.Security);
                holding.SecurityPrice = latestPrice;

                rawValue += holdingUnits * latestPrice.ClosingPrice;

                foreach (HoldingIncome income in holding.Income) {
                    holdingIncome += income.Income;
                }
                totalIncome += holdingIncome;
                holding.TotalIncome = holdingIncome;
                realisedGain += holding.RealisedGain;

                context.Update<Holding>(holding);
            }

            portfolio.CostBase = costBase;
            portfolio.CurrentRawValue = rawValue;
            portfolio.CurrentIncome = totalIncome;
            portfolio.TotalRealisedGain = realisedGain;
            portfolio.SoldCostBase = soldCostBase;

            context.Update<Portfolio>(portfolio);
            context.SaveChanges();
        }

        public void DownloadQuotes(Security security, DateTime purchaseDate, bool full)
        {
            //var prices = Yahoo.GetHistoricalAsync(security.YahooCode, purchaseDate, period: Period.Daily, ascending: true).Result;
            var prices = avClient.GetHistoricalPrices(security.YahooCode, purchaseDate, full).Result;
            foreach (DataPoint price in prices)
            {
                SecurityPrice savedPrice = context.SecurityPrices
                    .Where(s => s.Date.Date == price.Date)
                    .Where(s => s.Security == security).SingleOrDefault();

                if (savedPrice == null)
                {
                    SecurityPrice newPrice = new SecurityPrice()
                    {
                        SecurityId = security.Id,
                        Date = price.Date,
                        ClosingPrice = price.Close
                    };
                    context.Add<SecurityPrice>(newPrice);
                }
            }
            context.SaveChanges();
        }

        public void UpdateEndOfDayValues() {
            var securities = securityRepository.GetAll().ToList();
            foreach(Security security in securities) {
                var latestPrice = securityPriceRepository.GetLatestSecurityPrice(security);
                DownloadQuotes(security, latestPrice.Date.AddDays(1), false);
            }

            var portfolios = portfolioRepository.GetAll().ToList();
            foreach(Portfolio portfolio in portfolios) {
                UpdatePortfolioTotals(portfolio.Id);
            }
        }

        public void Dispose() {
            context.Dispose();
            securityPriceRepository.Dispose();
            securityRepository.Dispose();
            portfolioRepository.Dispose();
            holdingRepository.Dispose();
        }
    }
}