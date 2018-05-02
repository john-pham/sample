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
    public class MessengerViewModel
    {
        public Guid? MessengerId { get; set; }
        public Guid? BranchId { get; set; }
        public string MessengerCode { get; set; }
        public string MessengerName { get; set; }
        public string MessengerTitle { get; set; }
        public string BranchName { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreateDate { get; set; }
        public string ProfilerImage { get; set; }
        public FileViewModel Profile { get; set; }
    }
}
