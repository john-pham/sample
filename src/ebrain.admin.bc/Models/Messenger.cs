using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Messenger : HistoricalEntity
    {
        public Guid MessengerId { get; set; }
        public Guid? BranchId { get; set; }
        public string MessengerCode { get; set; }
        public string MessengerName { get; set; }
        public string MessengerTitle { get; set; }
    }
}