using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class PurchaseOrder : HistoricalEntity
    {
        public Guid PurchaseOrderId { get; set; }

        public string PurchaseOrderCode { get; set; }
        public Guid? BranchId { get; set; }
    }
}