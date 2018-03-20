using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class DeptDetail : HistoricalEntity
    {
        public Guid DeptDetailId { get; set; }

        public Guid? DeptId { get; set; }

        public Guid? StudentId { get; set; }

        public decimal? Receipt { get; set; }
        public decimal? Payment { get; set; }

    }
}