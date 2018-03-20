using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class IOStock : HistoricalEntity
    {
        public Guid IOStockId { get; set; }

        public string IONumber { get; set; }

        public long IOTypeId { get; set; }

        public Guid? SupplierId { get; set; }

        public Guid? BranchId { get; set; }

        public Guid? BranchParentId { get; set; }

        public string PurchaseOrderCode { get; set; }

        public Guid? PurchaseOrderId { get; set; }
        public Guid? StudentId { get; set; }
        public bool IsCancel { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalPriceBeforeVAT { get; set; }
    }
}