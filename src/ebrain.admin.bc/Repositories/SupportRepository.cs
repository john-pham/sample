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
using ebrain.admin.bc.Utilities;
using ebrain.admin.bc.Report;

namespace ebrain.admin.bc.Repositories
{
    public class SupportRepository : Repository<Support>, ISupportRepository
    {
        public int Total { get; private set; }
        public SupportRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Support> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Support> Search(string value, string branchIds, int page, int size)
        {
            try
            {
                var data = this.appContext.Support.Where(p => p.IsDeleted == false &&
                (string.IsNullOrEmpty(value) || p.SupportName.Contains(value)
                || p.Title.Contains(value)));

                this.Total = data.Count();

                //
                if (size > 0 && page >= 0)
                {
                    data = (from c in data
                            select c).Skip(page * size).Take(size);
                }

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Support> Save(Support value, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var itemExist = await Get(index, value.UpdatedBy);
            if (itemExist != null)
            {
                itemExist.SupportCode = value.SupportCode;
                itemExist.SupportName = value.SupportName;
                itemExist.Title = value.Title;
                itemExist.Email = value.Email;
                itemExist.Note = value.Note;
                itemExist.UpdatedBy = value.UpdatedBy;
                itemExist.UpdatedDate = DateTime.Now;
            }
            else
            {

                var result = await appContext.Support.AddAsync(value);
                itemExist = result.Entity;
            }
            await appContext.SaveChangesAsync();
            return itemExist;
        }

        public async Task<bool> Delete(Guid id)
        {
            var itemExist = appContext.Support.FirstOrDefault(p => p.SupportId == id);
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }

        public async Task<Support> Get(Guid? index, Guid userId)
        {
            return await this.appContext.Support.FirstOrDefaultAsync(p => p.SupportId == index);
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
