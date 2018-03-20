using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Report
{
    public class MaterialList
    {
        public Guid MaterialId { get; set; }

        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }

        public string TypeMaterialCode { get; set; }
        public string TypeMaterialName { get; set; }
        public string GrpMaterialCode { get; set; }
        public string GrpMaterialName { get; set; }
        public string Note { get; set; }
    }
}
