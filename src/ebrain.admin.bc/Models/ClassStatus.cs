using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class ClassStatus : HistoricalEntity
    {
        public Guid ClassStatusId { get; set; }
        [MaxLength(256)]
        public string ClassStatusCode { get; set; }
        [MaxLength(256)]
        public string ClassStatusName { get; set; }
        public Guid? BranchId { get; set; }
        
    }
}