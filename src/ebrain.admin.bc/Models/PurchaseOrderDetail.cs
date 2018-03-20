using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class PurchaseOrderDetail : HistoricalEntity
    {
        public Guid PurchaseOrderDetailId { get; set; }

        public Guid PurchaseOrderId { get; set; }

        public string MaterialId { get; set; }

        public string MaterialCode { get; set; }

        public string MaterialName { get; set; }

        public decimal InputQuantity { get; set; }

    }
}