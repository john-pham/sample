using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Report
{
    public class DeptList
    {
        public Guid StudentId { get; set; }
        public string StudentCode { get; set; }
        public string StudentName { get; set; }
        public string Phone { get; set; }
        public decimal ReceiptFirst { get; set; }
        public decimal PaymentFirst { get; set; }
        public decimal TotalPricePayment { get; set; }
        public decimal TotalPriceReceipt { get; set; }
        public decimal Payment { get; set; }
        public decimal Receipt { get; set; }
        public decimal EndPayment { get; set; }
        public decimal EndReceipt { get; set; }

    }
}
