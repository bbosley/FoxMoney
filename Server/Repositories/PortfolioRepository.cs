using System.Linq;
using FoxMoney.Server.Entities;
using FoxMoney.Server.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoxMoney.Server.Repositories
{
    public class PortfolioRepository : EntityBaseRepository<Portfolio>
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

        public Portfolio GetDetailedPortfolio(long id) {
            IQueryable<Portfolio> query = _context.Set<Portfolio>();
            return query.Where(p => p.Id == id)
                .Include(p => p.Holdings)
                    .ThenInclude(h => h.Security)
                .Include(p => p.Holdings)
                    .ThenInclude(h => h.SecurityPrice)
                .FirstOrDefault();
        }

    }
}