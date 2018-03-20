using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Unit : HistoricalEntity
    {
        public Guid UnitId { get; set; }

        public string UnitCode { get; set; }

        public string UnitName { get; set; }
        public Guid? BranchId { get; set; }
    }
}