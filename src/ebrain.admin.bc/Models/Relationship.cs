using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Relationship : HistoricalEntity
    {
        public Guid RelationshipId { get; set; }

        public string RelationshipCode { get; set; }

        public string RelationshipName { get; set; }
        
    }
}