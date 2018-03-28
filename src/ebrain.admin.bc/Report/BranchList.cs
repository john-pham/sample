using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Report
{
    public class BranchList
    {
        public Guid BranchId { get; set; }
        public Guid? ParentBranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string FAX { get; set; }
        public string Email { get; set; }
        public bool IsHQ { get; set; }
        public string LogoName { get; set; }
        public bool IsExist { get; set; }
    }
}