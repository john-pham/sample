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
    public class ProfitViewModel
    {
        public Guid? ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal TotalPriceFirst { get; set; }
        public decimal TotalPriceReceipt { get; set; }
        public decimal? TotalPricePayment { get; set; }
        public decimal? TotalPriceEnd { get; set; }
    }
}
