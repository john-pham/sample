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
    public class SupportViewModel
    {
        public Guid? SupportId { get; set; }
        public Guid? BranchId { get; set; }
        public string SupportCode { get; set; }
        public string SupportName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BranchName { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreateDate { get; set; }
        public string Note { get; set; }
    }
}
