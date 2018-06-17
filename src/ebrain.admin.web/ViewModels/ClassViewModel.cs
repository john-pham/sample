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
    public class ClassViewModel
    {
        public Guid? ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
        public string Note { get; set; }
        public Guid? BranchId { get; set; }

        public Guid? MaterialId { get; set; }
        public decimal? LongLearn { get; set; }
        public Guid? StatusId { get; set; }
        public decimal? MaxStudent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? StudentId { get; set; }
        public ClassStudentViewModel[] Students { get; set; }
        public ClassTimeViewModel[] Times { get; set; }

        public string FullName { get; set; }
        public string Address { get; set; }
        public string SupplierName { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CountStudent { get; set; }
        public Guid? IOStockId { get; set; }
        public Guid? IOStockDetailId { get; set; }
    }
}
