using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Gender : HistoricalEntity
    {
        public long GenderId { get; set; }

        public string GenderCode { get; set; }

        public string GenderName { get; set; }

        public Guid? BranchId { get; set; }
      
    }
}