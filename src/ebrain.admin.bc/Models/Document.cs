using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Document : HistoricalEntity
    {
        public Guid DocumentId { get; set; }

        public Guid? GroupDocumentId { get; set; }

        public Guid? BranchId { get; set; }

        public string DocumentCode { get; set; }

        public string DocumentName { get; set; }
        public string LinkWebSite { get; set; }

        public string Path { get; set; }
        [NotMapped]
        public string GroupDocumentName { get; set; }
    }
}