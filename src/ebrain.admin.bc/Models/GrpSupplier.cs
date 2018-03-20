using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class GrpSupplier : HistoricalEntity
    {
        public Guid GrpSupplierId { get; set; }

        public string GrpSupplierCode { get; set; }

        public string GrpSupplierName { get; set; }

        public Guid? BranchId { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsTeacher { get; set; }
    }
}