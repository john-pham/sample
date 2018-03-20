using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class GrpMaterial : HistoricalEntity
    {
        public Guid GrpMaterialId { get; set; }

        public string GrpMaterialCode { get; set; }

        public string GrpMaterialName { get; set; }

        public Guid? BranchId { get; set; }
        public Guid? TypeMaterialId { get; set; }

        //public virtual Branch Branch { get; set; }

        //public virtual ICollection<Material> Materials { get; set; }
    }
}