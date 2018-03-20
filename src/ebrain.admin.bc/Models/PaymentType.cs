using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class PaymentType : HistoricalEntity
    {
        public long PaymentTypeId { get; set; }

        public string PaymentTypeCode { get; set; }

        public string PaymentTypeName { get; set; }

        public Guid? BranchId { get; set; }
        public bool IsPayment { get; set; }

    }
}