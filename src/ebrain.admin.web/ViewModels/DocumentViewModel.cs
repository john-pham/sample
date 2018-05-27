﻿// ======================================
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
    public class DocumentViewModel
    {
        public Guid? ID { get; set; }
        public Guid? GrpId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
        public string Path { get; set; }
        public string GrDocumentName { get; set; }
        public string LinkWebSite { get; set; }
        public FileViewModel Logo { get; set; }
    }
}
