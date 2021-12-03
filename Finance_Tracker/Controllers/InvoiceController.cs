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
    public class InvoiceController : ControllerBase
    {
        public IInvoiceService _invoiceService;
        private readonly FinanceTrackerDB _financeTrackerDB;
        public InvoiceController(IInvoiceService invoiceService, FinanceTrackerDB financeTrackerDB)
        {
            _invoiceService = invoiceService;
            _financeTrackerDB = financeTrackerDB;
        }
        // GET: api/Invoices/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoiceById(Guid id)
        {
            var invoice = await _invoiceService.GetInvoiceById(id);
            {
                if (invoice == null)
                {
                    return NotFound();
                }
                return invoice;
            }
        }
        // GET: api/Invoice
        [HttpGet]
        public async Task<ActionResult> GetInvoice()
        {
            var invoices = await _invoiceService.GetInvoice();
            {
                if (invoices == null)
                {
                    return NotFound();
                }

                return Ok(invoices);
            }
        }
        // POST: api/Invoices
        [HttpPost]
        public async Task<ActionResult> CreateInvoice([FromBody] Invoice invoice)
        {
            try
            {
                _invoiceService.CreateInvoice(invoice);
                await _financeTrackerDB.SaveChangesAsync();
                return Ok(invoice);
            }
            catch
            {
                throw;
            }
        }
        // PUT: api/Invoices/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(Guid id, [FromBody] Invoice invoice)
        {
            try
            {
                if (id != invoice.Id)
                {
                    return BadRequest();
                }
                invoice.UpdatedAt = DateTime.Now;



                _financeTrackerDB.Entry(invoice).State = EntityState.Modified;

                await _financeTrackerDB.SaveChangesAsync();
                return Ok(invoice);
            }
            catch (Exception e)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(e);
                }
            }
        }
        private bool InvoiceExists(Guid id)
        {
            return _financeTrackerDB.Invoices.Any(e => e.Id == id);
        }

        // DELETE: api/Invoices/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInvoiceById(Guid id)
        {
            try
            {
                if (await _invoiceService.DeleteInvoiceById(id))
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
