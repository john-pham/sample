// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using ebrain.admin.bc.Models;
using ebrain.admin.bc.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Repositories.Interfaces
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        int Total { get; }

        Task<bool> Update(IEnumerable<UserRole> values);

        Task<bool> Delete(Guid id);

        Task<IList<BranchUser>> Search(string value, string branchIds, int page, int size);

        Task<IList<Report.AccessRight>> GetAll(Guid userId);
    }
}
