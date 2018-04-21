using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ebrain.admin.bc.Report
{
    public class AccessRight
    {
        public Guid GroupId { get; set; }
        public Guid FeatureId { get; set; }
        public Guid FeatureGroupId { get; set; }

        public string FeatureName { get; set; }

        public string GroupName { get; set; }

        public bool View { get; set; }

        public bool Edit { get; set; }

        public bool Delete { get; set; }

        public bool Create { get; set; }
        

    }
}
