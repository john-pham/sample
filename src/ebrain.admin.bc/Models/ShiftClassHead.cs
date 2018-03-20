using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class ShiftClassHead : HistoricalEntity
    {
        public Guid ShiftClassHeadId { get; set; }

        public Guid? BranchId { get; set; }

        public Guid? ShiftClassId { get; set; }
        
    }
}