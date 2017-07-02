using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoxMoney.Server.Entities;
using FoxMoney.Server.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoxMoney.Server.Repositories {
    public class ParcelRepository : EntityBaseRepository<Parcel> {
        private readonly ApplicationDbContext _context;
        public ParcelRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

        public virtual async Task<IEnumerable<Parcel>> AvailableParcelsAsync(int holdingId) {
            //return await _context.Set<Parcel>().Where(predicate).ToListAsync();
            IQueryable<Parcel> query = _context.Set<Parcel>();
            return await query.Where(p => p.HoldingId == holdingId && p.ParcelExhausted == false)
                .OrderByDescending(p => p.PurchasePrice)
                .ToListAsync();
        }
    }
}