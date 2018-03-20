// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using ebrain.admin.bc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Repositories.Interfaces
{
    public interface IPaymentTypeRepository : IRepository<PaymentType>
    {
        IEnumerable<PaymentType> GetTopActive(int count);

        Task<IEnumerable<PaymentType>> Search(string filter, string value, string branchIds);

        Task<PaymentType> Save(PaymentType value);
        Task<Boolean> Delete(string id);
    }
}
