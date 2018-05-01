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
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public int Total { get; private set; }
        public PaymentRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Payment> GetTopActive(int count, string branchIds)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<PaymentDetail>> GetDetailByIOId(Guid? id)
        {
            return await this.appContext.PaymentDetail.Where(p => p.IsDeleted == false && p.PaymentId == id).ToListAsync();
        }
        public async Task<Payment> FindById(Guid? id)
        {
            return await this.appContext.Payment.FirstOrDefaultAsync(p => p.IsDeleted == false && p.IsCancel == false && p.PaymentId == id);
        }

        public async Task<IEnumerable<Payment>> GetAlls(string branchIds)
        {
            return await this.appContext.Payment.Where(p => p.IsDeleted == false && p.IsCancel == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
        }

        public async Task<IEnumerable<Payment>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.Payment.Where(p => p.IsDeleted == false && p.IsCancel == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
        }

        public IEnumerable<PaymentList> GetPaymentList
            (DateTime fromDate, DateTime toDate, string filterValue, int paymentTypeId, bool isPayment, Guid? userAccessRightPerson, string branchIds
            , int page, int size)
        {
            try
            {
                List<PaymentList> someTypeList = new List<PaymentList>();
                this.appContext.LoadStoredProc("dbo.sp_PaymentSummarize")
                               .WithSqlParam("fromDate", fromDate)
                               .WithSqlParam("toDate", toDate)
                               .WithSqlParam("paymentTypeId", paymentTypeId)
                               .WithSqlParam("filterValue", filterValue)
                               .WithSqlParam("isPayment", isPayment)
                               .WithSqlParam("userAccessRightPerson", userAccessRightPerson.HasValue ? userAccessRightPerson.ToString() : null)
                               .WithSqlParam("branchIds", branchIds)
                               .ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<PaymentList>().ToList();
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
        public IEnumerable<PaymentDetailList> GetPaymentDetailList(
            DateTime fromDate, DateTime toDate, string filterValue, int paymentTypeId, bool isPayment, Guid? userAccessRightPerson
            , string branchIds, int page, int size)
        {
            try
            {
                List<PaymentDetailList> someTypeList = new List<PaymentDetailList>();
                this.appContext.LoadStoredProc("dbo.sp_PaymentDetail")
                               .WithSqlParam("fromDate", fromDate)
                               .WithSqlParam("toDate", toDate)
                               .WithSqlParam("paymentTypeId", paymentTypeId)
                               .WithSqlParam("filterValue", filterValue)
                               .WithSqlParam("isPayment", isPayment)
                               .WithSqlParam("userAccessRightPerson", userAccessRightPerson.HasValue ? userAccessRightPerson.ToString() : null)
                               .WithSqlParam("branchIds", branchIds)
                               .ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<PaymentDetailList>().ToList();
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
        public async Task<IEnumerable<PaymentType>> GetAllPaymentTypes(bool isPayment, string branchIds)
        {
            return await this.appContext.PaymentType.Where(p => p.IsPayment == isPayment).ToListAsync();
        }

        public async Task<Payment> Save(Payment value, PaymentDetail[] iosd, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var item = this.appContext.Payment.FirstOrDefault(p => p.PaymentId == index);
            if (item != null)
            {
                item.Note = value.Note;
                item.TotalMoney = value.TotalMoney;
                item.TotalMoneyAgain = value.TotalMoneyAgain;
                item.TotalMoneyReturn = value.TotalMoneyReturn;
                item.UpdatedBy = value.CreatedBy;
                item.UpdatedDate = DateTime.Now;

                var iodExists = await GetDetailByIOId(item.PaymentId);

                //update deleted
                var iodIds = iosd.Select(p => p.PaymentDetailId);
                var iodNotExists = iodExists.Where(p => !iodIds.Contains(p.PaymentDetailId));
                foreach (var itemDetail in iodNotExists)
                {
                    itemDetail.IsDeleted = true;
                }
                //Insert
                foreach (var itemDetail in iosd)
                {
                    var itemExistD = this.appContext.PaymentDetail.FirstOrDefault(p => p.PaymentDetailId == itemDetail.PaymentDetailId);
                    if (itemExistD != null)
                    {
                        itemExistD.PriceBeforeVAT = itemDetail.PriceBeforeVAT;
                        itemExistD.PriceAfterVAT = itemDetail.PriceAfterVAT;
                        itemExistD.TotalPrice = itemDetail.TotalPrice;
                        itemExistD.TotalPricePayment = itemDetail.TotalPricePayment;
                        itemExistD.TotalPriceExist = itemDetail.TotalPriceExist;
                    }
                    else
                    {
                        itemDetail.PaymentId = item.PaymentId;
                        await appContext.PaymentDetail.AddAsync(itemDetail);
                    }
                }

            }
            else
            {
                var result = await appContext.Payment.AddAsync(value);
                foreach (var itemD in iosd)
                {
                    itemD.PaymentId = value.PaymentId;
                    await appContext.PaymentDetail.AddAsync(itemD);
                }
                item = result.Entity;
            }
            //
            await appContext.SaveChangesAsync();
            return item;
        }

        public async Task<bool> Delete(string id)
        {
            //Check exist in materials
            var unitExist = this.appContext.Material.FirstOrDefault(p => p.UnitId.Equals(new Guid(id)));
            if (unitExist != null) throw new Exception("Exist in materials");

            var itemExist = appContext.Unit.FirstOrDefault(p => p.UnitId.Equals(new Guid(id)));
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }
        public async Task<Boolean> DeleteMaster(Guid? id)
        {
            var itemExist = appContext.Payment.FirstOrDefault(p => p.PaymentId == id);
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }
        public async Task<Boolean> CancelMaster(Guid? id)
        {
            var itemExist = appContext.Payment.FirstOrDefault(p => p.PaymentId == id);
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;

        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
