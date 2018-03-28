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
    public class BranchRepository : Repository<Branch>, IBranchRepository
    {
        public int Total { get; private set; }

        public BranchRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<BranchUser> GetAllBranchOfUser(Guid userId)
        {
            try
            {
                List<BranchUser> someTypeList = new List<BranchUser>();
                this.appContext.LoadStoredProc("dbo.sp_BranchCurrentOfUser")
                               .WithSqlParam("userId", userId).ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<BranchUser>().ToList();
                               });

                return someTypeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BranchList> GetBranchHead(string branchId)
        {
            try
            {
                List<BranchList> someTypeList = new List<BranchList>();
                this.appContext.LoadStoredProc("dbo.sp_BranchOfBranchHead")
                               .WithSqlParam("@branchId", branchId)
                               .ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<BranchList>().ToList();
                               });

                return someTypeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetAllBranchOfUserString(Guid userId)
        {
            return GetAllBranchOfUser(userId).Select(p => p.BranchId).ToArray().ConvertArrayGuidToString();
        }

        public async Task<IEnumerable<Branch>> Search(string filter, string value, int page, int size)
        {
            var list = from c in this.appContext.Branch
                       where !c.IsDeleted
                       select c;

            //
            this.Total = list.Count();

            if (size > 0 && page >= 0)
            {
                list = (from c in list
                        orderby c.CreatedDate descending
                        select c).Skip(page * size).Take(size);
            }

            return await list.ToListAsync();
        }

        public async Task<Branch> Get(Guid? index)
        {
            return await this.appContext.Branch.FirstOrDefaultAsync(p => p.BranchId == index);
        }

        public async Task<Branch> Save(Branch value, Guid? oldId)
        {
            var item = await appContext.Branch.FirstOrDefaultAsync(x => x.BranchId == oldId);

            if (item != null)
            {
                item.Address = value.Address;
                item.BranchCode = value.BranchCode;
                //item.BranchId 
                item.BranchName = value.BranchName;
                item.Email = value.Email;
                item.FAX = value.FAX;
                item.IsHQ = value.IsHQ;
                //item.LogoName = value.LogoName;
                item.Note = value.Note;
                item.PhoneNumber = value.PhoneNumber;
                item.UpdatedBy = value.UpdatedBy;
                item.UpdatedDate = value.UpdatedDate;
            }
            else
            {
                var result = await appContext.Branch.AddAsync(value);
                item = result.Entity;
            }
            //
            await appContext.SaveChangesAsync();
            //
            return item;
        }

        public async Task<Branch> SaveHead(Branch[] values, Guid? branchParentId, Guid userId)
        {
            var item = await Get(branchParentId);
            if (item != null)
            {
                foreach (var itemHead in values)
                {
                    var itemExistD = this.appContext.BranchHead.FirstOrDefault(
                            p => p.BranchParentId == branchParentId
                            && p.BranchId == itemHead.BranchId);

                    if (itemExistD != null)
                    {
                        itemExistD.IsDeleted = !itemHead.IsExist;
                    }
                    else
                    {
                        itemExistD = new BranchHead
                        {
                            BranchHeadId = Guid.NewGuid(),
                            BranchId = itemHead.BranchId,
                            BranchParentId = branchParentId,
                            CreatedBy = userId,
                            CreatedDate = DateTime.Now,
                            UpdatedBy = userId,
                            UpdatedDate = DateTime.Now,
                            IsDeleted = !itemHead.IsExist
                        };
                        await appContext.BranchHead.AddAsync(itemExistD);
                    }

                    await appContext.SaveChangesAsync();
                }
            }
            return item;
        }

        public async Task<bool> Delete(Guid id)
        {
            var itemExist = appContext.Branch.FirstOrDefault(p => p.BranchId == id);

            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            return await appContext.SaveChangesAsync() > 0;
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
