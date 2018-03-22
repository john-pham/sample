using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class ClassExamine : HistoricalEntity
    {
        public Guid ClassExamineId { get; set; }
        public Guid? ClassId { get; set; }
        public Guid? ExamineId { get; set; }
        public decimal Mark { get; set; }
    }
}