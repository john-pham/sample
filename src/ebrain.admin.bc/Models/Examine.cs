using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Examine : HistoricalEntity
    {
        public Guid ExamineId { get; set; }
        public Guid? BranchId { get; set; }
        public string ExamineCode { get; set; }
        public string ExamineName { get; set; }
    }
}