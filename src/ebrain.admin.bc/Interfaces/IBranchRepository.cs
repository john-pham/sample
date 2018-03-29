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
    public interface IBranchRepository : IRepository<Branch>
    {
        int Total { get; }

        Task<Branch> Get(Guid? index);
        Task<BranchHead> GetBranchHead(Guid? index);

        Task<IEnumerable<Branch>> Search(string filter, string value, int page, int size);

        Task<Branch> Save(Branch value, Guid? oldId);

        Task<Boolean> Delete(Guid id);

        string GetAllBranchOfUserString(Guid userId);

        IEnumerable<BranchUser> GetAllBranchOfUser(Guid userId);
        List<BranchList> GetBranchHead(string branchId);
        Task<Branch> SaveHead(Branch[] values, Guid? branchParentId, Guid userId);
    }
}
