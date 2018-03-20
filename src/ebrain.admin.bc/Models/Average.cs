using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Average : HistoricalEntity
    {
        public Guid AverageId { get; set; }
        //public virtual ICollection<AverageDetail> AverageDetails { get; set; }
    }
}