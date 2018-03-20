using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class ProfitDetail : HistoricalEntity
    {
        public Guid ProfitDetailId { get; set; }

        public Guid? ProfitId { get; set; }

        public Guid? BranchId { get; set; }

        public decimal? TotalPrice { get; set; }
        
    }
}