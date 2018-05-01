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
    public interface IDeptRepository : IRepository<Dept>
    {
        int Total { get; }
        IEnumerable<Dept> GetTopActive(int count);
        Task<Dept> FindById(Guid? id);
        Task<IEnumerable<Dept>> Search(string filter, string value);
        Task<IEnumerable<Dept>> GetAlls();
        Task<Dept> Save(Dept value, DeptDetail[] iosd, Guid? id);
        Task<Boolean> Delete(string id);
        Task<IEnumerable<DeptDetail>> GetDetailByIOId(Guid? id);
        Task<Boolean> DeleteMaster(Guid? id);
        Task<Boolean> CancelMaster(Guid? id);
        IEnumerable<DeptList> GetDeptList(DateTime fromDate, DateTime toDate, string filter, string branchIds, int page, int size);
        Task<bool> UpdateDept(DateTime fromDate, DateTime toDate, string filter, string branchIds, Guid userId);

    }
}
