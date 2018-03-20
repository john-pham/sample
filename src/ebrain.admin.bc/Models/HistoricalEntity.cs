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
    public class HistoricalEntity : IHistoricalEntity
    {
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AutoLogOnCode_LastUpdate_IP { get; set; }
        public string AutoLogOnCode_LastUpdate_ComputerName { get; set; }
        public string AutoLogOnCode_LastUpdate_MACAddress { get; set; }
        public bool IsDeleted { get; set; }
        //public byte[] TimeStamp { get; set; }
        public string Note { get; set; }
    }
}
