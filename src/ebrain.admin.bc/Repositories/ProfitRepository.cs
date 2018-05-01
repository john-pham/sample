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
    public class ProfitRepository : Repository<Profit>, IProfitRepository
    {
        public int Total { get; private set; }
        public ProfitRepository(ApplicationDbContext context) : base(context)
        { }



        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public Task<bool> CancelMaster(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMaster(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<Profit> FindById(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Profit>> GetAlls()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProfitDetail>> GetDetailByIOId(Guid? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProfitlList> GetProfitList(DateTime fromDate, DateTime toDate, string filter, string branchIds, int page, int size)
        {
            try
            {
                if (filter == null) filter = string.Empty;

                List<ProfitlList> someTypeList = new List<ProfitlList>();
                this.appContext.LoadStoredProc("dbo.sp_Profit")
                               .WithSqlParam("fromDate", fromDate)
                               .WithSqlParam("toDate", toDate)
                               .WithSqlParam("@branchIds", branchIds)
                               .WithSqlParam("@filterValue", filter).ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<ProfitlList>().ToList();
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

        public async Task<bool> UpdateProfit(DateTime fromDate, DateTime toDate, string filter, string branchIds, Guid userId)
        {
            try
            {
                ///Nếu như !=0 tức là trên 1 tháng!
                var months = (toDate.Year * 12 + toDate.Month) - (fromDate.Year * 12 + fromDate.Month);
                if (months < 0)
                {
                    throw new Exception("Should be ToDate than FromDate");
                }
                else
                {
                    ///Tính thời gian về cuối tháng
                    DateTime ToDate_LastMonth = toDate;
                    DateTime ToDate_ThisMonth = toDate;

                    //loop
                    for (var i = 0; i <= months; i++)
                    {
                        //nếu như khác giá trị ban đầu thì fromDate tăng lên 1 tháng
                        if (i != 0)
                        {
                            fromDate = fromDate.AddMonths(1);
                            ToDate_ThisMonth = new DateTime(fromDate.Year, fromDate.Month, 1);
                            toDate = ToDate_ThisMonth.AddMonths(1).AddSeconds(-1);//ve cuoi thang nay

                        }
                        else
                        {
                            //Khi trường hợp nó bằng 0 tức là đang ở khoảng fromDate, cần phải đưa nó về cuối tháng
                            //vụ đưa về cuối tháng mỗi thèng tự xử rùi không cần quan tâm làm gì?
                            toDate = fromDate;
                        }

                        toDate = toDate.AddMonths(1).AddSeconds(-1);//ve cuoi thang nay
                        var results = this.GetProfitList(fromDate, toDate, filter, branchIds, 0, 0);
                        if (results != null && results.Count() > 0)
                        {
                            var endDate = fromDate.AddMonths(1);
                            var month = endDate.Month;
                            var year = endDate.Year;

                            //get Profit
                            var inv = this.appContext.Profit.FirstOrDefault(
                                        p => p.CreatedDate.Month == month
                                        && p.CreatedDate.Year == year
                                        && p.IsDeleted == false);
                            //Initially ID
                            Guid ProfitId = Guid.NewGuid();
                            if (inv != null)
                            {
                                ProfitId = inv.ProfitId;
                            }
                            else
                            {
                                inv = new Profit
                                {
                                    ProfitId = ProfitId,
                                    CreatedBy = userId,
                                    CreatedDate = endDate,
                                    IsDeleted = false,
                                    Note = endDate.ToString(),
                                    UpdatedBy = userId,
                                    UpdatedDate = DateTime.Now
                                };
                                await this.appContext.AddAsync(inv);
                            }

                            var invds = this.appContext.ProfitDetail.Where(p => p.ProfitId == inv.ProfitId);
                            foreach (var item in results)
                            {
                                var itemExist = invds.FirstOrDefault(p => p.BranchId == item.BranchId);
                                if (itemExist != null)
                                {
                                    itemExist.TotalPrice = item.TotalPriceEnd;
                                    itemExist.UpdatedBy = userId;
                                    itemExist.UpdatedDate = DateTime.Now;
                                }
                                else
                                {
                                    itemExist = new ProfitDetail
                                    {
                                        ProfitDetailId = Guid.NewGuid(),
                                        ProfitId = ProfitId,
                                        TotalPrice = item.TotalPriceEnd,
                                        CreatedBy = userId,
                                        CreatedDate = DateTime.Now,
                                        BranchId = item.BranchId,
                                        IsDeleted = false,
                                        UpdatedBy = userId,
                                        UpdatedDate = DateTime.Now
                                    };
                                    await this.appContext.AddAsync(itemExist);
                                }

                            }
                            await this.appContext.SaveChangesAsync();
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Profit> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public Task<Profit> Save(Profit value, ProfitDetail[] iosd, Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Profit>> Search(string filter, string value)
        {
            throw new NotImplementedException();
        }
    }
}
