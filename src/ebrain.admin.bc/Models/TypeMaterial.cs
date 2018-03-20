using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class TypeMaterial : HistoricalEntity
    {
        public Guid TypeMaterialId { get; set; }

        public string TypeMaterialCode { get; set; }

        public string TypeMaterialName { get; set; }

        public Guid? BranchId { get; set; }
        public bool IsLearning { get; set; }
        public bool IsDocument { get; set; }
        //public virtual Branch Branch { get; set; }

        //public virtual ICollection<Material> Materials { get; set; }
    }
}