using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class AverageDetail : HistoricalEntity
    {
        public Guid AverageDetailId { get; set; }
        public Guid? AverageId { get; set; }
        public Guid? Materialid { get; set; }
        public Nullable<decimal> AveragePrice { get; set; }
        //public virtual Average Average { get; set; }
        //public virtual Material Material { get; set; }
    }
}