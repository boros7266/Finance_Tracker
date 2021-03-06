using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Finance_Tracker.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string Name { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        [ForeignKey("CostInvoiceId")]
        public Guid? CostInvoiceId { get; set; }
        [JsonIgnore]
        public CostInvoice CostInvoice { get; set; }
    }
}
