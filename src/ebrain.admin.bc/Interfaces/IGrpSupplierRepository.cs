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
    public interface IGrpSupplierRepository : IRepository<GrpSupplier>
    {
        int Total { get; }
        IEnumerable<GrpSupplier> GetTopActive(int count);

        Task<IEnumerable<GrpSupplier>> Search(string filter, string value, string branchIds, int page, int size);
        Task<IEnumerable<GrpSupplier>> GetAll(string branchIds, int option, int page, int size);
        Task<GrpSupplier> Get(Guid? index);
        Task<GrpSupplier> Save(GrpSupplier value, Guid? index);
        Task<Boolean> Delete(string id);
    }
}
