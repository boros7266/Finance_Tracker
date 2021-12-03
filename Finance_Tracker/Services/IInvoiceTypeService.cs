using Finance_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance_Tracker.Services
{
    public interface IInvoiceTypeService
    {
        Task<InvoiceType> GetInvoiceTypeById(Guid id);
        Task<InvoiceType[]> GetInvoiceType();
        void CreateInvoiceType([FromBody] InvoiceType invoiceType);
        Task<bool> DeleteInvoiceTypeById(Guid id);
    }
}
