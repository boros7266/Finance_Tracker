using Finance_Tracker.Models;
using Finance_Tracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        public ICompanyService _companyService;
        private readonly FinanceTrackerDB _financeTrackerDB;
        public CompanyController(ICompanyService companyService, FinanceTrackerDB financeTrackerDB)
        {
            _companyService = companyService;
            _financeTrackerDB = financeTrackerDB;
        }
        // GET: api/Companys/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompanyById(Guid id)
        {
            var company = await _companyService.GetCompanyById(id);
            {
                if (company == null)
                {
                    return NotFound();
                }
                return company;
            }
        }
        // GET: api/Companys
        [HttpGet]
        public async Task<ActionResult> GetCompany()
        {
            var companys = await _companyService.GetCompany();
            {
                if (companys == null)
                {
                    return NotFound();
                }

                return Ok(companys);
            }
        }
        // POST: api/Companys
        [HttpPost]
        public async Task<ActionResult> CreateCompany([FromBody] Company company)
        {
            try
            {
                _companyService.CreateCompany(company);
                await _financeTrackerDB.SaveChangesAsync();
                return Ok(company);
            }
            catch
            {
                throw;
            }
        }
        // DELETE: api/Companys/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompanyById(Guid id)
        {
            try
            {
                if (await _companyService.DeleteCompanyById(id))
                {
                    await _financeTrackerDB.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound(id.ToString());
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
