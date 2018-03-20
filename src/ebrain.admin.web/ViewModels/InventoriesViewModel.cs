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
    public class InventoriesViewModel
    {
        public Guid? ID { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string GrpName { get; set; }
        public string Code { get; set; }
        public string TypeCode { get; set; }
        public string GrpCode { get; set; }
        public decimal QuantityInv { get; set; }
        public decimal QuantityInput { get; set; }
        public decimal? QuantityOutput { get; set; }
        public decimal? QuantityEnd { get; set; }

    }
}
