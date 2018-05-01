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
    public interface IShiftClassRepository : IRepository<ShiftClass>
    {
        int Total { get; }
        IEnumerable<ShiftClass> GetTopActive(int count);

        Task<IEnumerable<ShiftClass>> Search(string filter, string value, string branchIds, int page, int size);
        Task<ShiftClass> Get(Guid? index);
        Task<ShiftClass> Save(ShiftClass value, Guid? index);
        Task<Boolean> Delete(string id);
    }
}
