using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class ClassHead : HistoricalEntity
    {
        public Guid ClassHeadId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? ClassId { get; set; }
        //public virtual Class Classes { get; set; }
    }
}