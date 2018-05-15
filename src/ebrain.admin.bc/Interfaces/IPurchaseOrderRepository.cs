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
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        int Total { get; }
        IEnumerable<PurchaseOrder> GetTopActive(int count);
        Task<PurchaseOrder> FindById(Guid? id);
        Task<IEnumerable<PurchaseOrder>> Search(string filter, string value, string branchIds);
        Task<IEnumerable<PurchaseOrder>> GetAlls(string branchIds);
        Task<PurchaseOrder> Save(PurchaseOrder value, PurchaseOrderDetail[] iosd, Guid? id);
        Task<IEnumerable<PurchaseOrderDetail>> GetDetailByIOId(Guid? id);
        Task<Boolean> DeleteMaster(Guid? id);
        Task<Boolean> CancelMaster(Guid? id);

        IEnumerable<PurchaseOrderList> GetPurchaseOrderList(DateTime fromDate, DateTime toDate, string ioNumber, string branchIds, int page, int size, bool isShowToInput);
        IEnumerable<PurchaseOrderList> GetPurchaseOrderListDetail(DateTime fromDate, DateTime toDate, string filterValue, string branchIds, string ioStockId, int page, int size);
        IEnumerable<PurchaseOrderList> GetPurchaseOrderListDetailHistory(DateTime fromDate, DateTime toDate, string filterValue, string branchIds, int page, int size);
    }
}
