using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Student : HistoricalEntity
    {
        public Guid StudentId { get; set; }

        public string StudentCode { get; set; }

        public string StudentName { get; set; }

        public string Address { get; set; }

        public string TaxCode { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string AccountBank { get; set; }

        public Guid? BranchId { get; set; }

        public long GenderId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string SchoolName { get; set; }

        public string ClassName { get; set; }
        public DateTime Birthday { get; set; }
        public Guid? StudentStatusId { get; set; }
    }
}