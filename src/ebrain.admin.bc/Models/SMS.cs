using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class SMS : HistoricalEntity
    {
        public Guid SMSId { get; set; }
        public Guid? BranchId { get; set; }
        public string Phone { get; set; }
        public string Body { get; set; }
        public string Result { get; set; }
        [NotMapped]
        public string BranchName { get; set; }
    }
}