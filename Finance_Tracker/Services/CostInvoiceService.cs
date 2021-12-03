using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Finance_Tracker.Services
{
    public class CostInvoiceService : ICostInvoiceService
    {
        private readonly FinanceTrackerDB _financeTrackerDB;
        public CostInvoiceService(FinanceTrackerDB financeTrackerDB)
        {
            _financeTrackerDB = financeTrackerDB;
        }
        public async Task<CostInvoice> GetCostInvoiceById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _financeTrackerDB.CostInvoices
                .Include(x => x.Projects)
                .FirstOrDefaultAsync(predicate: b => b.Id == id);
        }
        public async Task<CostInvoice[]> GetCostInvoice()
        {
            return await _financeTrackerDB.CostInvoices
                .Include(x => x.Projects)
                .OrderByDescending(x => x.CreatedAt)
                .ToArrayAsync();
        }
        public void CreateCostInvoice([FromBody] CostInvoice costInvoice)
        {
            _financeTrackerDB.CostInvoices.Add(new CostInvoice
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Name = costInvoice.Name
            });
        }
        public async Task<bool> DeleteCostInvoiceById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var delete = await _financeTrackerDB.CostInvoices.FindAsync(id);
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
