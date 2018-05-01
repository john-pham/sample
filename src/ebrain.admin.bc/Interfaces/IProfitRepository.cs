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
    public interface IProfitRepository : IRepository<Profit>
    {
        int Total { get; }
        IEnumerable<Profit> GetTopActive(int count);
        Task<Profit> FindById(Guid? id);
        Task<IEnumerable<Profit>> Search(string filter, string value);
        Task<IEnumerable<Profit>> GetAlls();
        Task<Profit> Save(Profit value, ProfitDetail[] iosd, Guid? id);
        Task<Boolean> Delete(string id);
        Task<IEnumerable<ProfitDetail>> GetDetailByIOId(Guid? id);
        Task<Boolean> DeleteMaster(Guid? id);
        Task<Boolean> CancelMaster(Guid? id);
        IEnumerable<ProfitlList> GetProfitList(DateTime fromDate, DateTime toDate, string filter, string branchIds, int page, int size);
        Task<bool> UpdateProfit(DateTime fromDate, DateTime toDate, string filter, string branchIds, Guid userId);

    }
}
