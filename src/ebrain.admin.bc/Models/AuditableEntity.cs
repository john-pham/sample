// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ebrain.admin.bc.Models.Interfaces;

namespace ebrain.admin.bc.Models
{
    public class AuditableEntity : IAuditableEntity
    {
        [MaxLength(256)]
        public Guid? CreatedBy { get; set; }

        [MaxLength(256)]
        public Guid? UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
