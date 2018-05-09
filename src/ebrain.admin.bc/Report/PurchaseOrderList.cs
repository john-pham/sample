﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Report
{
    public class PurchaseOrderList
    {
        public Guid PurchaseOrderId { get; set; }
        public string PurchaseOrderCode { get; set; }
        public string FullName { get; set; }
        public string BranchName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Note { get; set; }
        public decimal PurchaseQuantity { get; set; }
        public decimal IOQuantity { get; set; }
        public Guid PurchaseOrderDetailId { get; set; }
        public Guid MaterialId { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        
    }
}
