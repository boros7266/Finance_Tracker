using Finance_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Tracker.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly FinanceTrackerDB _financeTrackerDB;
        public PartnerService(FinanceTrackerDB financeTrackerDB)
        {
            _financeTrackerDB = financeTrackerDB;
        }
        public async Task<Partner> GetPartnerById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _financeTrackerDB.Partners.Include(x => x.Invoices).FirstOrDefaultAsync(predicate: b => b.Id == id);
        }
        public async Task<Partner[]> GetPartner()
        {
            return await _financeTrackerDB.Partners
                .Include(x => x.Invoices)
                .OrderByDescending(x => x.CreatedAt)
                .ToArrayAsync();
        }
        public void CreatePartner([FromBody] Partner partner)
        {
            _financeTrackerDB.Partners.Add(new Partner
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Name = partner.Name
            });
        }
        public async Task<bool> DeletePartnerById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var delete = await _financeTrackerDB.Partners.FindAsync(id);
            if (delete == null)
            {
                return false;
            }
            else
            {
                _financeTrackerDB.Remove(delete);
                return true;
            }
        }
    }
}
