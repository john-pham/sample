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

namespace ebrain.admin.bc.Models.Interfaces
{
    public interface IHistoricalEntity
    {
        string AutoLogOnCode_LastUpdate_IP { get; set; }
        string AutoLogOnCode_LastUpdate_ComputerName { get; set; }
        string AutoLogOnCode_LastUpdate_MACAddress { get; set; }
        bool IsDeleted { get; set; }
        //byte[] TimeStamp { get; set; }
        string Note { get; set; }
        Guid CreatedBy { get; set; }
        Guid UpdatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
