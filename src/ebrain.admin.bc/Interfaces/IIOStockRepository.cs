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
    public interface IIOStockRepository : IRepository<IOStock>
    {
        int Total { get; }
        IEnumerable<IOStock> GetTopActive(int count);
        Task<IOStock> FindById(Guid? id);
        Task<IEnumerable<IOStock>> Search(string filter, string value, string branchIds);
        Task<IEnumerable<IOStock>> GetAlls(string branchIds);
        Task<IOStock> Save(IOStock value, IOStockDetail[] iosd, Guid? id);
        Task<bool> SaveDept(IOStockDetail[] iosd);
        Task<Boolean> Delete(string id);
        Task<IEnumerable<IOStockDetail>> GetDetailByIOId(Guid? id);
        Task<Boolean> DeleteMaster(Guid? id);
        Task<Boolean> CancelMaster(Guid? id);
        IEnumerable<IOStockList> GetIOStockList(DateTime fromDate, DateTime toDate, string ioNumber, int ioTypeId, string branchIds, int page, int size);
        IEnumerable<IOStockDetailList> GetIOStockDetailList(DateTime fromDate, DateTime toDate, string ioNumber, int ioTypeId, string branchIds, int page, int size);
        IEnumerable<IOStockDetailList> GetIOStockDetailListDept
            (Guid? studentId, Guid? ioStockId, string filterValue, bool isGetDept, string branchIds, int page, int size);
        IEnumerable<IOStockDetailList> GetWarehouseCard(DateTime fromDate, DateTime toDate, string filterValue, int ioTypeId, string branchIds, int page, int size);
        IEnumerable<IOStockListPayment> GetIOStockPaymentList(DateTime fromDate, DateTime toDate, string filterValue, string ioId, int ioTypeId,
            bool isInput, bool isWaitingClass, string branchIds, int page, int size);
        IEnumerable<IOStockListPayment> GetIOStockPaymentListDetail(DateTime fromDate, DateTime toDate, string filterValue, string ioId, int ioTypeId, bool isInput,
            bool isWaitingClass, bool isLearning, string branchIds, int page, int size);
    }
}
