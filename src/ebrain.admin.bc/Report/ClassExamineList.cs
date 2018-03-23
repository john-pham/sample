using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Report
{
    public class ClassExamineList
    {
        public Guid ClassExamineId { get; set; }
        public Guid? ClassId { get; set; }
        public Guid? ExamineId { get; set; }
        public Guid? StudentId { get; set; }
        public string ExamineCode { get; set; }
        public string ExamineName { get; set; }
        public decimal Mark { get; set; }
    }
}
