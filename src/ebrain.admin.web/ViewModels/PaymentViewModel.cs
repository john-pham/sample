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
    public class PaymentViewModel
    {
        public Guid? ID { get; set; }
        public string Code { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? BranchId { get; set; }
        public string BranchName { get; set; }
        public Guid? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string Note { get; set; }
        public decimal Quantity { get; set; }
        public decimal SellPrice { get; set; }
        public decimal? TotalMoney { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalMoneyAgain { get; set; }
        public decimal? TotalMoneyReturn { get; set; }
        public PaymentDetailViewModel[] IODetails { get; set; }
        public Guid? StudentId { get; set; }
        public long PaymentTypeId { get; set; }
        public string FullName { get; set; }
        public string PaymentTypeName { get; set; }
        public Guid? IOStockId { get; set; }
        public string IONumber { get; set; }

        public string CreateDate_Grp { get; set; }
        public string CreateDate_MMYY
        {
            get
            {
                return CreateDate.ToString("dd/MM");
            }
        }
    }
}
