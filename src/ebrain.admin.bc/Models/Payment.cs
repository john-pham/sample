using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Payment : HistoricalEntity
    {
        public Guid PaymentId { get; set; }

        public long PaymentTypeId { get; set; }

        public string PaymentCode { get; set; }

        public string PaymentName { get; set; }

        public Guid? BranchId { get; set; }

        public decimal? TotalMoney { get; set; }

        public decimal? TotalMoneyReturn { get; set; }

        public decimal? TotalMoneyAgain { get; set; }
        
        public bool IsCancel { get; set; }
    }
}