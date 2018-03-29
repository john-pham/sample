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
    public class SMSViewModel
    {
        public Guid? ID { get; set; }
        public Guid? BranchId { get; set; }
        public string Phone { get; set; }
        public string Body { get; set; }
        public string BranchName { get; set; }
        public string Result { get; set; }
    }
}
