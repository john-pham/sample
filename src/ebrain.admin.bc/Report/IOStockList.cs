using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Report
{
    public class IOStockList
    {
        public string BranchName { get; set; }
        public Guid IOStockId { get; set; }
        public string IONumber { get; set; }
        public long IOTypeId { get; set; }
        public decimal? TotalPrice { get; set; }
        public string FullName { get; set; }
        public string StudentName { get; set; }
        public Guid StudentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Note { get; set; }
    }
}
