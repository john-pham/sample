using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class ShiftClass : HistoricalEntity
    {
        public Guid ShiftClassId { get; set; }

        public string ShiftClassCode { get; set; }

        public string ShiftClassName { get; set; }

        public Guid? BranchId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}