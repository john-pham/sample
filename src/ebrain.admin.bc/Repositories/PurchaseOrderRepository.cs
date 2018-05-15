// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ebrain.admin.bc.Models;
using ebrain.admin.bc.Repositories.Interfaces;
using ebrain.admin.bc.Report;
using ebrain.admin.bc.Utilities;

namespace ebrain.admin.bc.Repositories
{
    public class PurchaseOrderRepository : Repository<PurchaseOrder>, IPurchaseOrderRepository
    {
        public int Total { get; private set; }
        public PurchaseOrderRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<PurchaseOrder> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<PurchaseOrderDetail>> GetDetailByIOId(Guid? id)
        {
            return await this.appContext.PurchaseOrderDetail.Where(p => p.IsDeleted == false && p.PurchaseOrderId == id).ToListAsync();
        }
        public async Task<PurchaseOrder> FindById(Guid? id)
        {
            return await this.appContext.PurchaseOrder.FirstOrDefaultAsync(p => p.IsDeleted == false && p.IsCancel == false && p.PurchaseOrderId == id);
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAlls(string branchIds)
        {
            return await this.appContext.PurchaseOrder.Where(p => p.IsDeleted == false && p.IsCancel == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
        }

        public async Task<IEnumerable<PurchaseOrder>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.PurchaseOrder.Where(p => p.IsDeleted == false && p.IsCancel == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
        }

        public async Task<PurchaseOrder> Save(PurchaseOrder value, PurchaseOrderDetail[] iosd, Guid? index)
        {
            //get branchId
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            //HeadQuater purchase order material
            var itemHQ = await this.appContext.Branch.FirstOrDefaultAsync(p => p.IsHQ == true && p.IsDeleted == false);
            if (itemHQ != null)
            {
                value.BranchPurchaseId = itemHQ.BranchId;
            }

            var item = this.appContext.PurchaseOrder.FirstOrDefault(p => p.PurchaseOrderId == index);
            if (item != null)
            {
                item.Note = value.Note;
                item.CreatedDate = value.CreatedDate;
                item.TotalPrice = value.TotalPrice;
                item.TotalPriceBeforeVAT = value.TotalPriceBeforeVAT;

                var iodExists = await GetDetailByIOId(item.PurchaseOrderId);

                //update deleted
                var iodIds = iosd.Select(p => p.PurchaseOrderDetailId);
                var iodNotExists = iodExists.Where(p => !iodIds.Contains(p.PurchaseOrderDetailId));
                foreach (var itemDetail in iodNotExists)
                {
                    itemDetail.IsDeleted = true;
                }
                //Insert
                foreach (var itemDetail in iosd)
                {
                    var itemExistD = this.appContext.PurchaseOrderDetail.FirstOrDefault(p => p.PurchaseOrderDetailId == itemDetail.PurchaseOrderDetailId);
                    if (itemExistD != null)
                    {
                        itemExistD.InputQuantity = itemDetail.InputQuantity;
                        itemExistD.PriceBeforeVAT = itemDetail.PriceBeforeVAT;
                        itemExistD.PriceAfterVAT = itemDetail.PriceAfterVAT;
                        itemExistD.TotalPrice = itemDetail.TotalPrice;
                        itemExistD.TotalPriceBeforeVAT = itemDetail.TotalPriceBeforeVAT;
                    }
                    else
                    {
                        itemDetail.PurchaseOrderId = item.PurchaseOrderId;
                        await appContext.PurchaseOrderDetail.AddAsync(itemDetail);
                    }
                }
            }
            else
            {
                var result = await appContext.PurchaseOrder.AddAsync(value);
                foreach (var itemD in iosd)
                {
                    await appContext.PurchaseOrderDetail.AddAsync(itemD);
                }
                item = result.Entity;
            }
            //
            await appContext.SaveChangesAsync();
            return item;
        }

        public async Task<Boolean> DeleteMaster(Guid? id)
        {
            var itemExist = appContext.PurchaseOrder.FirstOrDefault(p => p.PurchaseOrderId == id);
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }
        public async Task<Boolean> CancelMaster(Guid? id)
        {
            var itemExist = appContext.PurchaseOrder.FirstOrDefault(p => p.PurchaseOrderId == id);
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;

        }

        public IEnumerable<PurchaseOrderList> GetPurchaseOrderList(DateTime fromDate, DateTime toDate, string filterValue, string branchIds, int page, int size, 
            bool isShowToInput)
        {
            try
            {
                List<PurchaseOrderList> someTypeList = new List<PurchaseOrderList>();
                this.appContext.LoadStoredProc("dbo.sp_PurchaseOrderList")
                               .WithSqlParam("fromDate", fromDate)
                               .WithSqlParam("toDate", toDate)
                               .WithSqlParam("branchIds", branchIds)
                               .WithSqlParam("filterValue", filterValue).ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<PurchaseOrderList>().ToList();
                               });

                if(isShowToInput == true)
                {
                    someTypeList = someTypeList.Where(item => (item.PurchaseQuantity - item.IOQuantity) > 0).ToList();
                }
                //paging
                this.Total = someTypeList.Count();
                if (size > 0 && page >= 0)
                {
                    someTypeList = (from c in someTypeList select c).Skip(page * size).Take(size).ToList();
                }

                return someTypeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PurchaseOrderList> GetPurchaseOrderListDetail(DateTime fromDate, DateTime toDate, string filterValue, string branchIds, string ioStockId, int page, int size)
        {
            try
            {
                List<PurchaseOrderList> someTypeList = new List<PurchaseOrderList>();
                this.appContext.LoadStoredProc("dbo.sp_PurchaseOrderListDetail")
                               .WithSqlParam("fromDate", fromDate)
                               .WithSqlParam("toDate", toDate)
                               .WithSqlParam("branchIds", branchIds)
                                .WithSqlParam("@ioStockId", string.IsNullOrEmpty(ioStockId) ? null : ioStockId)
                               .WithSqlParam("filterValue", filterValue).ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<PurchaseOrderList>().ToList();
                               });

                //paging
                this.Total = someTypeList.Count();
                if (size > 0 && page >= 0)
                {
                    someTypeList = (from c in someTypeList select c).Skip(page * size).Take(size).ToList();
                }

                return someTypeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PurchaseOrderList> GetPurchaseOrderListDetailHistory(DateTime fromDate, DateTime toDate, string filterValue, string branchIds, int page, int size)
        {
            try
            {
                List<PurchaseOrderList> someTypeList = new List<PurchaseOrderList>();
                this.appContext.LoadStoredProc("dbo.sp_PurchaseOrderListHistory")
                               .WithSqlParam("fromDate", fromDate)
                               .WithSqlParam("toDate", toDate)
                               .WithSqlParam("branchIds", branchIds)
                               .WithSqlParam("filterValue", filterValue).ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<PurchaseOrderList>().ToList();
                               });

                //paging
                this.Total = someTypeList.Count();
                if (size > 0 && page >= 0)
                {
                    someTypeList = (from c in someTypeList select c).Skip(page * size).Take(size).ToList();
                }

                return someTypeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
