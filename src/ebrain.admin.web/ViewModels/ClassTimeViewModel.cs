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
    public class ClassTimeViewModel
    {
        public Guid ID { get; set; }
        public Guid? ClassId { get; set; }
        public Guid? BranchId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? OnTodayId { get; set; }
        public Guid? RoomId { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? ShiftId { get; set; }
    }
}
