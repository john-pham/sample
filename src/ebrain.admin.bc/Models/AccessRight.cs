using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ebrain.admin.bc.Models
{
    public class AccessRight : Interfaces.IAuditableEntity
    {
        public Guid AccessRightId { get; set; }
        public Guid GroupID { get; set; }
        public Guid FeatureID { get; set; }
        public System.Int16? Value { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
