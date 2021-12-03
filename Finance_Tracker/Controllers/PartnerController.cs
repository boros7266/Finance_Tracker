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
    public class PartnerController : ControllerBase
    {
        public IPartnerService _partnerService;
        private readonly FinanceTrackerDB _financeTrackerDB;
        public PartnerController(IPartnerService partnerService, FinanceTrackerDB financeTrackerDB)
        {
            _partnerService = partnerService;
            _financeTrackerDB = financeTrackerDB;
        }
        // GET: api/Partners/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Partner>> GetPartnerById(Guid id)
        {
            var partner = await _partnerService.GetPartnerById(id);
            {
                if (partner == null)
                {
                    return NotFound();
                }
                return partner;
            }
        }
        // GET: api/Partners
        [HttpGet]
        public async Task<ActionResult> GetPartner()
        {
            var partners = await _partnerService.GetPartner();
            {
                if (partners == null)
                {
                    return NotFound();
                }

                return Ok(partners);
            }
        }
        // POST: api/Partners
        [HttpPost]
        public async Task<ActionResult> CreatePartner([FromBody] Partner partner)
        {
            try
            {
                _partnerService.CreatePartner(partner);
                await _financeTrackerDB.SaveChangesAsync();
                return Ok(partner);
            }
            catch
            {
                throw;
            }
        }
        // PUT: api/Partners/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartner(Guid id, [FromBody] Partner partner)
        {
            try
            {
                if (id != partner.Id)
                {
                    return BadRequest();
                }
                partner.UpdatedAt = DateTime.Now;

                _financeTrackerDB.Entry(partner).State = EntityState.Modified;

                await _financeTrackerDB.SaveChangesAsync();
                return Ok(partner);
            }
            catch (Exception e)
            {
                if (!PartnerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(e);
                }
            }
        }
        private bool PartnerExists(Guid id)
        {
            return _financeTrackerDB.Partners.Any(e => e.Id == id);
        }
        // DELETE: api/Partners/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePartnerById(Guid id)
        {
            try
            {
                if (await _partnerService.DeletePartnerById(id))
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
