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

namespace ebrain.admin.bc.Repositories
{
    public class ShiftClassRepository : Repository<ShiftClass>, IShiftClassRepository
    {
        public ShiftClassRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<ShiftClass> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<ShiftClass>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.ShiftClass.Where(p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
        }

        public async Task<ShiftClass> Save(ShiftClass value, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var itemExist = await Get(index);
            if (itemExist != null)
            {
                itemExist.ShiftClassCode = value.ShiftClassCode;
                itemExist.ShiftClassName = value.ShiftClassName;
                itemExist.Note = value.Note;
                itemExist.UpdatedBy = value.UpdatedBy;
                itemExist.UpdatedDate = DateTime.Now;
            }
            else
            {
                var result = await appContext.ShiftClass.AddAsync(value);
                itemExist = result.Entity;
            }
            await appContext.SaveChangesAsync();
            return itemExist;
        }

        public async Task<bool> Delete(string id)
        {
            var itemExist = appContext.ShiftClass.FirstOrDefault(p => p.ShiftClassId.Equals(new Guid(id)));
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }

        public Task<ShiftClass> Get(Guid? index)
        {
            return this.appContext.ShiftClass.FirstOrDefaultAsync(p => p.ShiftClassId == index);
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
