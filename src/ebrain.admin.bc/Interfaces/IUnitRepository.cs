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
    public interface IUnitRepository : IRepository<Unit>
    {
        int Total { get; }
        IEnumerable<Unit> GetTopActive(int count);
        Task<Unit> FindById(Guid? id);
        Task<IEnumerable<Unit>> Search(string filter, string value, string branchIds, int page, int size);
        Task<IEnumerable<Unit>> GetAllUnits(string branchIds);
        Task<Unit> Save(Unit value, Guid? id);
        Task<Boolean> Delete(string id);
    }
}
