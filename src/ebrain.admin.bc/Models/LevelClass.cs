using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class LevelClass : HistoricalEntity
    {
        public Guid LevelClassId { get; set; }

        public string LevelClassCode { get; set; }

        public string LevelClassName { get; set; }

        public Guid? BranchId { get; set; }
    }
}