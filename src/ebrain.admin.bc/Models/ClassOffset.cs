using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class ClassOffset : HistoricalEntity
    {
        public Guid ClassOffsetId { get; set; }
        public Guid? ClassId { get; set; }
        public Guid? OnTodayId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? ShiftId { get; set; }
        public DateTime? LearnDate { get; set; }
    }
}