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
    public class AccessRightViewModel
    {
        public Guid FeatureID { get; set; }
        public Guid GroupID { get; set; }

        public string GroupName { get; set; }

        public string FeatureName { get; set; }

        public bool View { get; set; }

        public bool Edit { get; set; }

        public bool Delete { get; set; }

        public bool Create { get; set; }
        
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }

        public string FullName { get; set; }
        public string UserName { get; set; }
        public string BranchName { get; set; }
    }
}
