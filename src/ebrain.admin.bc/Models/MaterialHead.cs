using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class MaterialHead : HistoricalEntity
    {
        public Guid MaterialHeadId { get; set; }

        public Guid? BranchId { get; set; }

        public Guid? MaterialId { get; set; }

        public int? VAT { get; set; }

        public decimal? Price { get; set; }

        public decimal? PriceAfterVAT { get; set; }

        public decimal? WholePrice { get; set; }

        public decimal? WholePriceAfterVAT { get; set; }

        public decimal? SellPrice { get; set; }

        public decimal? SellPriceAfterVAT { get; set; }

        public decimal? MaskPassCourse { get; set; }

        public decimal? NumberHourse { get; set; }

        public string CalBeCourse { get; set; }
        public string SpBeCourse { get; set; }
        public string CalEnCourse { get; set; }
        public string SpEnCourse { get; set; }

    }
}