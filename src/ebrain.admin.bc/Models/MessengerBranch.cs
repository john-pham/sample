using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class MessengerBranch : HistoricalEntity
    {
        public Guid MessengerBranchId { get; set; }
        public Guid? MessengerId { get; set; }
        public Guid? BranchId { get; set; }
    }
}