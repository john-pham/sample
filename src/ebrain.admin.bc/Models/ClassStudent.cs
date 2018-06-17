using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class ClassStudent : HistoricalEntity
    {
        public Guid ClassStudentId { get; set; }
        public Guid? ClassId { get; set; }
        public Guid? IOStockId { get; set; }
        public Guid? IOStockDetailId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? MaterialId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}