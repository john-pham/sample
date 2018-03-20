using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class IOType : HistoricalEntity
    {
        public long IOTypeId { get; set; }

        public string IOTypeCode { get; set; }

        public string IOTypeName { get; set; }

        public bool IsInput { get; set; }

        public bool JoinStockMovementSummary { get; set; }

        public bool JoinAverages { get; set; }
    }
}