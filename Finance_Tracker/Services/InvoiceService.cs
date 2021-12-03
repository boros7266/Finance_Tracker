using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finance_Tracker.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly FinanceTrackerDB _financeTrackerDB;
        public InvoiceService(FinanceTrackerDB financeTrackerDB)
        {
            _financeTrackerDB = financeTrackerDB;
        }
        public async Task<Invoice> GetInvoiceById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _financeTrackerDB.Invoices
                .Include(x => x.InvoiceType)
                .Include(x => x.Project)
                .Include(x => x.Partner)
                .FirstOrDefaultAsync(predicate: b => b.Id == id);
        }
        public async Task<Invoice[]> GetInvoice()
        {
            return await _financeTrackerDB.Invoices
                .Include(x => x.InvoiceType)
                .Include(x => x.Project)
                .Include(x => x.Partner)
                .OrderByDescending(x => x.CreatedAt)
                .ToArrayAsync();
        }
        public void CreateInvoice([FromBody] Invoice invoice)
        {
                _financeTrackerDB.Invoices.Add(new Invoice
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    InvoiceNumber = invoice.InvoiceNumber,
                    DueDate = invoice.DueDate,
                    PaymentDate = invoice.PaymentDate,
                    Amount = invoice.Amount,
                    Currency = invoice.Currency,
                    Rate = invoice.Rate
                });
        }
        public async Task<bool> DeleteInvoiceById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var delete = await _financeTrackerDB.Invoices.FindAsync(id);
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
