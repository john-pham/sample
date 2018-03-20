using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class StudentRelationShip : HistoricalEntity
    {
        public Guid StudentRelationShipId { get; set; }

        public Guid? StudentId { get; set; }

        public string Address { get; set; }

        public string TaxCode { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string AccountBank { get; set; }
        
        public Guid? BranchId { get; set; }

        public Guid? RelationShipId { get; set; }

        public string RelationRequire { get; set; }

        public string Facebook { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public DateTime? Birthday { get; set; }
    }
}