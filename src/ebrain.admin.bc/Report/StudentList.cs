﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Report
{
    public class StudentList
    {
        public Guid? StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
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
        public string ClassName { get; set; }
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
        
    }
}