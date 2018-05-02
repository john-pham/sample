using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Report
{
    public class MessengerReport
    {
        public Guid MessengerId { get; set; }
        public Guid? BranchId { get; set; }
        public string MessengerCode { get; set; }
        public string MessengerName { get; set; }
        public string MessengerTitle { get; set; }
        public string BranchName { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreateDate { get; set; }
        public string ProfilerImage { get; set; }
    }
}
