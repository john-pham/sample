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
    public class StudentViewModel
    {
        public Guid? ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public string Taxcode { get; set; }
        public string Phone { get; set; }
        public string AccountBank { get; set; }
        public string Email { get; set; }
        public long GenderId { get; set; }
        public Guid? StudentStatusId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SchoolName { get; set; }
        public Guid? ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public DateTime Birthday { get; set; }
        public string Avatar { get; set; }
        public Guid? SupplierId { get; set; }
        public string TypeMaterialId { get; set; }
        public string GrpMaterialId { get; set; }
        public int TotalDay { get; set; }
        public string FaUsername { get; set; }
        public DateTime? FaBirthday { get; set; }
        public string FaPhone { get; set; }
        public string FaFacebook { get; set; }
        public string FaAddress { get; set; }
        public string FaEmail { get; set; }
        public string FaJob { get; set; }
        public string FaRelationship { get; set; }
        public string Fawanted { get; set; }
        public string GenderName { get; set; }

        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ShiftClassName { get; set; }
        public string TodayName { get; set; }
        public string RoomName { get; set; }
        public int? CountAbsent { get; set; }
        public int? CountNotAbsent { get; set; }
    }
}
