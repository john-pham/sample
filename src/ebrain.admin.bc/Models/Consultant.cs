using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Consultant : HistoricalEntity
    {
        public Guid ConsultantId { get; set; }
        public string ConsultantCode { get; set; }
        public string ConsultantName { get; set; }
        public Guid? BranchId { get; set; }
    }
}