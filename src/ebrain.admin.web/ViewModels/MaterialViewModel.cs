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
    public class MaterialViewModel
    {
        public Guid? ID { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string GrpName { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? TypeMaterialId { get; set; }
        public Guid GrpMaterialId { get; set; }
        public Guid? SupplierId { get; set; }

        public decimal? Price { get; set; }
        public decimal? SellPrice { get; set; }
        public decimal? WholePrice { get; set; }

        public decimal? SellPriceAfterVAT { get; set; }

        public decimal? MaskPassCourse { get; set; }

        public decimal? NumberHourse { get; set; }

        public string CalBeCourse { get; set; }
        public string SpBeCourse { get; set; }
        public string CalEnCourse { get; set; }
        public string SpEnCourse { get; set; }
    }
}
