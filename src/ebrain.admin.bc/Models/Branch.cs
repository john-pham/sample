using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Branch : HistoricalEntity
    {
        public Guid BranchId { get; set; }
        [MaxLength(256)]
        public string BranchCode { get; set; }
        [MaxLength(256)]
        public string BranchName { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string FAX { get; set; }
        [MaxLength(256)]
        public string Email { get; set; }
        public bool IsHQ { get; set; }
        [MaxLength(256)]
        public string LogoName { get; set; }
    }
}