using System.Linq;
using FoxMoney.Server.Entities;
using FoxMoney.Server.Repositories;

namespace FoxMoney.Server.Repositories
{
    public class SecurityPriceRepository : EntityBaseRepository<SecurityPrice>
    {
        private readonly ApplicationDbContext _context;
        public SecurityPriceRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

        public SecurityPrice GetLatestSecurityPrice(Security security)
        {
            IQueryable<SecurityPrice> query = _context.Set<SecurityPrice>();
            return query.Where(s => s.Security == security)
                .OrderByDescending(s => s.Date)
                .FirstOrDefault();
        }

    }
}