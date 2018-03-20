using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Report
{
    public class PaymentDetailList
    {
        public Guid PaymentId { get; set; }
        public Guid IOStockId { get; set; }
        public string IONumber { get; set; }
        public string PaymentCode { get; set; }
        public long PaymentTypeId { get; set; }
        public decimal? TotalPrice { get; set; }
        public string FullName { get; set; }
        public string PaymentTypeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Note { get; set; }
    }
}
