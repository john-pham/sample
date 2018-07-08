using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class FeatureNoResult : Interfaces.IAuditableEntity
    {
        public Guid FeatureNoResultId { get; set; }
        public string Results { get; set; }
        public Guid? BranchId { get; set; }
        public string RecepientEmail { get; set; }
        public string SenderEmails { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
