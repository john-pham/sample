using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class PromotionDetail : HistoricalEntity
    {
        public Guid PromotionDetailId { get; set; }

        public Guid? PromotionId { get; set; }

        public Guid? MaterialId { get; set; }

        public string MaterialCode { get; set; }

        public string MaterialName { get; set; }

        public int? DiscountPercent { get; set; }

        public decimal? DiscountMoney { get; set; }
        
    }
}