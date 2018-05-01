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
    public interface IInventoriesRepository : IRepository<Inventory>
    {
        int Total { get; }
        IEnumerable<Inventory> GetTopActive(int count);
        Task<Inventory> FindById(Guid? id);
        Task<IEnumerable<Inventory>> Search(string filter, string value);
        Task<IEnumerable<Inventory>> GetAlls();
        Task<Inventory> Save(Inventory value, InventoryDetail[] iosd, Guid? id);
        Task<Boolean> Delete(string id);
        Task<IEnumerable<InventoryDetail>> GetDetailByIOId(Guid? id);
        Task<Boolean> DeleteMaster(Guid? id);
        Task<Boolean> CancelMaster(Guid? id);
        IEnumerable<InventorieslList> GetInventoryList(DateTime fromDate, DateTime toDate, string filter, string branchIds, int page, int size);
        Task<bool> UpdateInventory(DateTime fromDate, DateTime toDate, string filter, string branchIds, Guid userId);

    }
}
