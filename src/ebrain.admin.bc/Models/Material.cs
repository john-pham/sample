using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Material : HistoricalEntity
    {
        public Guid MaterialId { get; set; }

        public Guid GrpMaterialId { get; set; }

        public Guid UnitId { get; set; }

        public string MaterialCode { get; set; }

        public string MaterialName { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? SupplierId { get; set; }
    }
}