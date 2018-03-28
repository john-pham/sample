using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class BranchSMS : HistoricalEntity
    {
        public Guid BranchSMSId { get; set; }
        public Guid BranchId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CPCode { get; set; }
        public string RequestID { get; set; }
        public string ServiceId { get; set; }
        public string CommandCode { get; set; }
        public string ContentType { get; set; }
    }
}