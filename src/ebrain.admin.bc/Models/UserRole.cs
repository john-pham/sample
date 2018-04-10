using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ebrain.admin.bc.Models
{
    public class UserRole : Interfaces.IAuditableEntity
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
