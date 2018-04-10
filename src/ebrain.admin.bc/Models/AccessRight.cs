using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ebrain.admin.bc.Models
{
    public class AccessRight : Interfaces.IAuditableEntity
    {
        public Guid GroupID { get; set; }
        public Guid FeatureID { get; set; }
        public System.Int16? Value { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
