using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class GroupDocument : HistoricalEntity
    {
        public Guid GroupDocumentId { get; set; }

        public string GroupDocumentCode { get; set; }

        public string GroupDocumentName { get; set; }

        public Guid? BranchId { get; set; }
    }
}