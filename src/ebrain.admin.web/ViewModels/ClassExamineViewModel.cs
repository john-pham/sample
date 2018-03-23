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
    public class ClassExamineViewModel
    {
        public Guid ClassExamineId { get; set; }
        public Guid? ClassId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? ExamineId { get; set; }
        public string ExamineCode { get; set; }
        public string ExamineName { get; set; }
        public decimal Mark { get; set; }
    }
}
