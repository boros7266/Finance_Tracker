using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance_Tracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Tracker.Services
{
    public interface ICostInvoiceService
    {
        Task<CostInvoice> GetCostInvoiceById(Guid id);
        Task<CostInvoice[]> GetCostInvoice();
        void CreateCostInvoice([FromBody] CostInvoice costInvoice);
        Task<bool> DeleteCostInvoiceById(Guid id);
    }
}
