using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Finance_Tracker.Models
{
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        [StringLength(50)]
        public string InvoiceNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        [StringLength(3)]
        public string Currency { get; set; }
        public decimal? Rate { get; set; }

        [ForeignKey("InvoiceTypeId")]
        public Guid? InvoiceTypeId { get; set; }
        [JsonIgnore]
        public InvoiceType InvoiceType { get; set; }

        [ForeignKey("ProjectId")]
        public Guid? ProjectId { get; set; }
        [JsonIgnore]
        public Project Project { get; set; }

        [ForeignKey("PartnerId")]
        public Guid? PartnerId { get; set; }
        [JsonIgnore]
        public Partner Partner { get; set; }
        [ForeignKey("PartnerId")]
        public Guid? CompanyId { get; set; }
        [JsonIgnore]
        public Company Company { get; set; }
    }
}
