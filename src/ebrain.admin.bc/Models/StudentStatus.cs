using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class StudentStatus : HistoricalEntity
    {
        public Guid StudentStatusId { get; set; }

        public string StudentStatusCode { get; set; }

        public string StudentStatusName { get; set; }

        public Guid? BranchId { get; set; }
      
    }
}