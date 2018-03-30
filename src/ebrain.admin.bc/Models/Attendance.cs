using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Attendance : HistoricalEntity
    {
        public Guid AttendanceId { get; set; }
        public Guid ClassId { get; set; }
        public Guid StudentId { get; set; }
        public Guid? BranchId { get; set; }
        public bool Absent { get; set; }
        public DateTime AttendanceDate { get; set; }
    }
}