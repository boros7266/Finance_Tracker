using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance_Tracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Tracker.Services
{
    public interface ICompanyService
    {
        Task<Company> GetCompanyById(Guid id);
        Task<Company[]> GetCompany();
        void CreateCompany([FromBody] Company company);
        Task<bool> DeleteCompanyById(Guid id);
    }
}
