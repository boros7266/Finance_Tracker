using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Finance_Tracker.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly FinanceTrackerDB _financeTrackerDB;
        public CompanyService(FinanceTrackerDB financeTrackerDB)
        {
            _financeTrackerDB = financeTrackerDB;
        }
        public async Task<Company> GetCompanyById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _financeTrackerDB.Companys
                .Include(x => x.Invoices)
                .FirstOrDefaultAsync(predicate: b => b.Id == id);
        }
        public async Task<Company[]> GetCompany()
        {
            return await _financeTrackerDB.Companys
                .Include(x => x.Invoices)
                .OrderByDescending(x => x.CreatedAt)
                .ToArrayAsync();
        }
        public void CreateCompany([FromBody] Company company)
        {
            _financeTrackerDB.Companys.Add(new Company
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Name = company.Name
            });
        }
        public async Task<bool> DeleteCompanyById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var delete = await _financeTrackerDB.Companys.FindAsync(id);
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
