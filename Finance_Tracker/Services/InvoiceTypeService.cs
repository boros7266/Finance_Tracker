using Finance_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance_Tracker.Services
{
    public class InvoiceTypeService : IInvoiceTypeService
    {
        private readonly FinanceTrackerDB _financeTrackerDB;
        public InvoiceTypeService(FinanceTrackerDB financeTrackerDB)
        {
            _financeTrackerDB = financeTrackerDB;
        }
        public async Task<InvoiceType> GetInvoiceTypeById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _financeTrackerDB.InvoiceTypes.Include(x => x.Invoices).FirstOrDefaultAsync(predicate: b => b.Id == id);
        }
        public async Task<InvoiceType[]> GetInvoiceType()
        {
            return await _financeTrackerDB.InvoiceTypes
                .Include(x => x.Invoices)
                .OrderByDescending(x => x.CreatedAt)
                .ToArrayAsync();
        }
        public void CreateInvoiceType([FromBody] InvoiceType invoiceType)
        {
            _financeTrackerDB.InvoiceTypes.Add(new InvoiceType
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Type = invoiceType.Type
            });
        }
        public async Task<bool> DeleteInvoiceTypeById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var delete = await _financeTrackerDB.InvoiceTypes.FindAsync(id);
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
