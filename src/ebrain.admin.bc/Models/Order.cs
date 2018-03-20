// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Order : AuditableEntity
    {
        public int Id { get; set; }
        public decimal Discount { get; set; }
        public string Comments { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }


        public string CashierId { get; set; }
        public ApplicationUser Cashier { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }


        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
