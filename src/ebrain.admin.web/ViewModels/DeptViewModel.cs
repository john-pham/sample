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
    public class DeptViewModel
    {
        public Guid StudentId { get; set; }
        public string StudentCode { get; set; }
        public string StudentName { get; set; }
        public string Phone { get; set; }
        public decimal ReceiptFirst { get; set; }
        public decimal PaymentFirst { get; set; }
        public decimal TotalPricePayment { get; set; }
        public decimal TotalPriceReceipt { get; set; }
        public decimal Payment { get; set; }
        public decimal Receipt { get; set; }
        public decimal EndPayment { get; set; }
        public decimal EndReceipt { get; set; }

    }
}
