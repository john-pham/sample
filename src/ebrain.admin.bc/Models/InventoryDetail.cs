using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class InventoryDetail : HistoricalEntity
    {
        public Guid InventoryDetailId { get; set; }

        public Guid? InventoryId { get; set; }

        public Guid? Materialid { get; set; }

        public decimal? InputQuantity { get; set; }
        
    }
}