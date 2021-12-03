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
    public class InvoiceTypeController : ControllerBase
    {
        public IInvoiceTypeService _invoiceTypeService;
        private readonly FinanceTrackerDB _financeTrackerDB;
        public InvoiceTypeController(IInvoiceTypeService invoiceTypeService, FinanceTrackerDB financeTrackerDB)
        {
            _invoiceTypeService = invoiceTypeService;
            _financeTrackerDB = financeTrackerDB;
        }
        // GET: api/InvoiceTypes/id
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceType>> GetInvoiceTypeById(Guid id)
        {
            var invoiceType = await _invoiceTypeService.GetInvoiceTypeById(id);
            {
                if (invoiceType == null)
                {
                    return NotFound();
                }
                return invoiceType;
            }
        }
        // GET: api/InvoiceTypes
        [HttpGet]
        public async Task<ActionResult> GetInvoiceType()
        {
            var invoiceTypes = await _invoiceTypeService.GetInvoiceType();
            {
                if (invoiceTypes == null)
                {
                    return NotFound();
                }

                return Ok(invoiceTypes);
            }
        }
        // POST: api/InvoiceTypes
        [HttpPost]
        public async Task<ActionResult> CreateInvoiceType([FromBody] InvoiceType invoiceType)
        {
            try
            {
                _invoiceTypeService.CreateInvoiceType(invoiceType);
                await _financeTrackerDB.SaveChangesAsync();
                return Ok(invoiceType);
            }
            catch
            {
                throw;
            }
        }
        // PUT: api/InvoiceTypes/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceType(Guid id, [FromBody] InvoiceType invoiceType)
        {
            try
            {
                if (id != invoiceType.Id)
                {
                    return BadRequest();
                }
                invoiceType.UpdatedAt = DateTime.Now;

                _financeTrackerDB.Entry(invoiceType).State = EntityState.Modified;

                await _financeTrackerDB.SaveChangesAsync();
                return Ok(invoiceType);
            }
            catch (Exception e)
            {
                if (!InvoiceTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(e);
                }
            }
        }
        private bool InvoiceTypeExists(Guid id)
        {
            return _financeTrackerDB.InvoiceTypes.Any(e => e.Id == id);
        }
        // DELETE: api/InvoiceTypes/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInvoiceTypeById(Guid id)
        {
            try
            {
                if (await _invoiceTypeService.DeleteInvoiceTypeById(id))
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
