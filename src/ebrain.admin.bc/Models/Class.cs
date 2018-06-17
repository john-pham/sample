using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Class : HistoricalEntity
    {
        public Guid ClassId { get; set; }
        [MaxLength(256)]
        public string ClassCode { get; set; }
        [MaxLength(256)]
        public string ClassName { get; set; }
        public Guid? BranchId { get; set; }

        public Guid? MaterialId { get; set; }
        public decimal? LongLearn { get; set; }
        public Guid? StatusId { get; set; }
        public decimal? MaxStudent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? SupplierId { get; set; }
        [NotMapped]
        public Guid? IOStockId { get; set; }
        [NotMapped]
        public Guid? IOStockDetailId { get; set; }

    }
}