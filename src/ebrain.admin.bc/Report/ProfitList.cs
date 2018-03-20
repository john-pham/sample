using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Report
{
    public class ProfitlList
    {
        public Guid? BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public decimal TotalPriceFirst { get; set; }
        public decimal TotalPriceReceipt { get; set; }
        public decimal? TotalPricePayment { get; set; }
        public decimal? TotalPriceEnd { get; set; }
    }
}
