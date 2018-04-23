using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ebrain.admin.bc.Models
{
    public class AccessRightPerson : HistoricalEntity
    {
        public Guid FeatureId { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
    }
}
