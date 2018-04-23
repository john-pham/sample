using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ebrain.admin.bc.Report
{
    public class AccessRightPerson
    {
        public Guid FeatureId { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }

        public string FullName { get; set; }
        public string UserName { get; set; }
        public string BranchName { get; set; }
    }
}
