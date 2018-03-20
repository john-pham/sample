using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class PaymentDetail : HistoricalEntity
    {
        public Guid PaymentDetailId { get; set; }

        public Guid? PaymentId { get; set; }

        public Guid? IOStockId { get; set; }

        public string IONumber { get; set; }

        public Guid? BranchId { get; set; }

        public int? VAT { get; set; }

        public decimal? PriceBeforeVAT { get; set; }

        public decimal? PriceAfterVAT { get; set; }

        public decimal? TotalPrice { get; set; }
        public decimal? TotalPricePayment { get; set; }
        public decimal? TotalPriceExist { get; set; }
    }
}