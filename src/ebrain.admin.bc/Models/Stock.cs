using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Stock : HistoricalEntity
    {
        public Guid StockId { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }
        public Guid? BranchId { get; set; }
    }
}