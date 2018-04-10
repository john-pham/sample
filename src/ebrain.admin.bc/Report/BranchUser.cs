using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Report
{
    public class BranchUser
    {
        public Guid BranchId { get; set; }
        public string UserId { get; set; }
        public string GroupId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool? IsActive { get; set; }
        public string BranchName { get; set; }
        public string  GroupName { get; set; }
    }
}
