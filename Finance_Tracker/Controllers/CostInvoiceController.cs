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
    public class CostInvoiceController : ControllerBase
    {
        public ICostInvoiceService _costInvoiceService;
        private readonly FinanceTrackerDB _financeTrackerDB;
        public CostInvoiceController(ICostInvoiceService costInvoiceService, FinanceTrackerDB financeTrackerDB)
        {
            _costInvoiceService = costInvoiceService;
            _financeTrackerDB = financeTrackerDB;
        }
        // GET: api/CostInvoices/id
        [HttpGet("{id}")]
        public async Task<ActionResult<CostInvoice>> GetCostInvoiceById(Guid id)
        {
            var costInvoice = await _costInvoiceService.GetCostInvoiceById(id);
            {
                if (costInvoice == null)
                {
                    return NotFound();
                }
                return costInvoice;
            }
        }
        // GET: api/CostInvoices
        [HttpGet]
        public async Task<ActionResult> GetCostInvoice()
        {
            var costInvoices = await _costInvoiceService.GetCostInvoice();
            {
                if (costInvoices == null)
                {
                    return NotFound();
                }

                return Ok(costInvoices);
            }
        }
        // POST: api/CostInvoices
        [HttpPost]
        public async Task<ActionResult> CreateCostInvoice([FromBody] CostInvoice costInvoice)
        {
            try
            {
                _costInvoiceService.CreateCostInvoice(costInvoice);
                await _financeTrackerDB.SaveChangesAsync();
                return Ok(costInvoice);
            }
            catch
            {
                throw;
            }
        }
        // DELETE: api/CostInvoices/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCostInvoiceById(Guid id)
        {
            try
            {
                if (await _costInvoiceService.DeleteCostInvoiceById(id))
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
