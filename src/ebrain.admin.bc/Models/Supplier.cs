using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Supplier : HistoricalEntity
    {
        public Guid SupplierId { get; set; }

        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public string Address { get; set; }

        public string TaxCode { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string AccountBank { get; set; }

        public Guid? BranchId { get; set; }

        public Guid? GrpSupplierId { get; set; }
        public DateTime? Birthday { get; set; }
        public Guid? UserLoginId { get; set; }
    }
}