using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Support : HistoricalEntity
    {
        public Guid SupportId { get; set; }
        public Guid? BranchId { get; set; }
        public string SupportCode { get; set; }
        public string SupportName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
    }
}