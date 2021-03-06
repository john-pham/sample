﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Report
{
    public class IOStockDetailList
    {
        public Guid IOStockId { get; set; }
        public Guid IOStockDetailId { get; set; }
        public Guid MaterialId { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public decimal InputQuantity { get; set; }
        public string BranchName { get; set; }
        public decimal QuantityInput { get; set; }
        public decimal QuantityOutput { get; set; }
        public string IONumber { get; set; }
        public long IOTypeId { get; set; }
        public decimal? TotalPrice { get; set; }
        public string FullName { get; set; }
        public string StudentName { get; set; }
        public Guid StudentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Note { get; set; }
        public decimal? InputExport { get; set; }
        public DateTime? DateExport { get; set; }
        public Guid? ByExport { get; set; }
        public string FullNameExport { get; set; }
    }
}
