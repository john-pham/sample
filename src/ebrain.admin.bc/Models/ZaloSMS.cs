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
    public class ZaloSMS : HistoricalEntity
    {
        public Guid ZaloSMSId { get; set; }
        public Guid BranchId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
        public string AppId { get; set; }
        public string Secret { get; set; }
        public string CallBackUrl { get; set; }
        public string HomeUrl { get; set; }
    }
}