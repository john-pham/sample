using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class ClassTime : HistoricalEntity
    {
        public Guid ClassTimeId { get; set; }
        public Guid? ClassId { get; set; }
        public Guid? BranchId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? OnTodayId { get; set; }
        public Guid? RoomId { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? ShiftId { get; set; }
    }
}