﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Report
{
    public class Feature
    {
        public Guid ID { get; set; }
        public Guid? GroupID { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public Guid? Reference { get; set; }
        public Guid? ReferenceItem { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
