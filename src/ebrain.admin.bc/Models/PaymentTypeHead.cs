using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class PaymentTypeHead : HistoricalEntity
    {
        public Guid PaymentTypeHeadId { get; set; }

        public Guid? BranchId { get; set; }

        public Guid? PaymentTypeId { get; set; }
        
    }
}