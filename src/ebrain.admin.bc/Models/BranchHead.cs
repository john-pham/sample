using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class BranchHead : HistoricalEntity
    {
        public Guid BranchHeadId { get; set; }

        public Guid? BranchParentId { get; set; }

        public Guid? BranchId { get; set; }
    }
}