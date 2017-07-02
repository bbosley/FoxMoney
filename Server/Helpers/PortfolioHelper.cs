using FoxMoney.Server.Entities;
using FoxMoney.Server.Repositories.Abstract;
using FoxMoney.Server.ViewModels;

namespace FoxMoney.Server.Helpers {
    public class PortfolioHelper {
        private IEntityBaseRepository<Portfolio> portfolioRepository;
        private IEntityBaseRepository<SecurityPrice> securityPriceRepository;
        private IEntityBaseRepository<Security> securityRepository;
        private IEntityBaseRepository<Holding> holdingRepository;
        private IEntityBaseRepository<Parcel> parcelRepository;

        public PortfolioHelper(IEntityBaseRepository<Portfolio> portfolioRepository,
            IEntityBaseRepository<SecurityPrice> securityPriceRepository,
            IEntityBaseRepository<Security> securityRepository,
            IEntityBaseRepository<Holding> holdingRepository,
            IEntityBaseRepository<Parcel> parcelRepository) {
            this.portfolioRepository = portfolioRepository;
            this.securityPriceRepository = securityPriceRepository;
            this.securityRepository = securityRepository;
            this.holdingRepository = holdingRepository;
            this.parcelRepository = parcelRepository;
        }

        public void CreatePortfolioComponents(Portfolio portfolio, SavePortfolioViewModel portfolioDetails) {
            
        }
    }
}