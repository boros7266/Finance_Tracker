using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance_Tracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Tracker.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> GetInvoiceById(Guid id);
        Task<Invoice[]> GetInvoice();
        void CreateInvoice([FromBody] Invoice invoice);
        Task<bool> DeleteInvoiceById(Guid id);
    }
}
