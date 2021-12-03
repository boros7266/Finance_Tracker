using Finance_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance_Tracker.Services
{
    public class ProjectService : IProjectService
    {
        private readonly FinanceTrackerDB _financeTrackerDB;
        public ProjectService(FinanceTrackerDB financeTrackerDB)
        {
            _financeTrackerDB = financeTrackerDB;
        }
        public async Task<Project> GetProjectById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _financeTrackerDB.Projects
                .Include(x => x.Invoices)
                .Include(x => x.CostInvoice)
                .FirstOrDefaultAsync(predicate: b => b.Id == id);
        }
        public async Task<Project[]> GetProject()
        {
            return await _financeTrackerDB.Projects
                .Include(x => x.Invoices)
                .Include(x => x.CostInvoice)
                .OrderByDescending(x => x.CreatedAt)
                .ToArrayAsync();
        }
        public void CreateProject([FromBody] Project project)
        {
            _financeTrackerDB.Projects.Add(new Project
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Name = project.Name
            });
        }
        public async Task<bool> DeleteProjectById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var delete = await _financeTrackerDB.Projects.FindAsync(id);
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
