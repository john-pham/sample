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
    public interface ISupplierRepository : IRepository<Supplier>
    {
        IEnumerable<Supplier> GetTopActive(int count);

        Task<IEnumerable<Supplier>> Search(string filter, string value, string branchIds,int isOption);

        Task<Supplier> Save(Supplier value, Guid? index);
        Task<Boolean> Delete(string id);
        Task<Supplier> Get(Guid? index);
    }
}
