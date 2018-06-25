using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class IOStockDetail : HistoricalEntity
    {
        public Guid IOStockDetailId { get; set; }

        public Guid? IOStockId { get; set; }

        public Guid? MaterialId { get; set; }

        public string MaterialCode { get; set; }

        public string MaterialName { get; set; }

        public decimal InputQuantity { get; set; }

        public int? VAT { get; set; }

        public decimal? PriceBeforeVAT { get; set; }

        public decimal? PriceAfterVAT { get; set; }

        public Guid? PurchaseOrderDetailId { get; set; }
        public Guid? PurchaseOrderId { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? TotalPriceBeforeVAT { get; set; }
        public decimal? InputExport { get; set; }
        public DateTime? DateExport { get; set; }
        public Guid? ByExport { get; set; }
    }
}