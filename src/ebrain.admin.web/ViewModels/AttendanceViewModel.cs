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
    public class AttendanceViewModel
    {
        public Guid AttendanceId { get; set; }
        public Guid ClassId { get; set; }
        public Guid StudentId { get; set; }
        public Guid? BranchId { get; set; }
        public bool Absent { get; set; }
        public bool NotAbsent { get; set; }
        public string StudentCode { get; set; }
        public string StudentName { get; set; }
        public string Phone { get; set; }
        public DateTime AttendanceDate { get; set; }
        public bool? IsAttendance { get; set; }
    }
}
