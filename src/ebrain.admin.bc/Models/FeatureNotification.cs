using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class FeatureNotification : Interfaces.IAuditableEntity
    {
        public Guid FeatureNotificationId { get; set; }
        public Guid? FeatureId { get; set; }
        public Guid? BranchId { get; set; }
        public string TemplateEmail { get; set; }
        public string Emails { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
