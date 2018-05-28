using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Report
{
    public class AttendanceList
    {
        public bool? IsAttendance { get; set; }
        public Guid AttendanceId { get; set; }
        public Guid ClassId { get; set; }
        public Guid StudentId { get; set; }
        public Guid? BranchId { get; set; }
        public bool Absent { get; set; }
        public bool NotAbsent { get; set; }
        public string StudentCode { get; set; }
        public string StudentName { get; set; }
        public string Phone { get; set; }
        public DateTime AttendanceDate { get; set; }
    }
}
