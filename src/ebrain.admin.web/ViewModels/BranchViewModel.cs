// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Ebrain.ViewModels
{
    public class BranchViewModel
    {
        public Guid? ID { get; set; }
        public Guid? ParentBranchId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Fax { get; set; }

        public FileViewModel Logo { get; set; }
        public bool IsExist { get; set; }
    }
}
