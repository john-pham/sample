﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Report
{
    public class ClassList
    {
        public Guid ClassId { get; set; }
        public Guid? ClassOffsetId { get; set; }
        public Guid? ClassExId { get; set; }
        public Guid? StudentId { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? ShiftId { get; set; }
        public Guid? MaterialId { get; set; }
        public decimal? LongLearn { get; set; }
        public Guid? StatusId { get; set; }
        public decimal? MaxStudent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CountStudent { get; set; }
        public DateTime? LearnDate { get; set; }
    }
}
