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
    public class UserRoleViewModel
    {
        public Guid? ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public Guid BranchId { get; set; }
        public Guid? UserId { get; set; }
        public string GroupId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string GroupName { get; set; }
        public string BranchName { get; set; }
    }
}
