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
    public class LevelClassRepository : Repository<LevelClass>, ILevelClassRepository
    {
        public int Total { get; private set; }
        public LevelClassRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<LevelClass> GetTopActive(int count, string branchIds)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<LevelClass>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.LevelClass.Where(p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
        }

        public async Task<LevelClass> Save(LevelClass value, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var itemExist = await Get(index);
            if (itemExist != null)
            {
                itemExist.LevelClassCode = value.LevelClassCode;
                itemExist.LevelClassName = value.LevelClassName;
                itemExist.Note = value.Note;
                itemExist.UpdatedBy = value.UpdatedBy;
                itemExist.UpdatedDate = DateTime.Now;
            }
            else
            {
                var result = await appContext.LevelClass.AddAsync(value);
                itemExist = result.Entity;
            }
            await appContext.SaveChangesAsync();
            //
            return itemExist;
        }

        public async Task<bool> Delete(string id)
        {
            var itemExist = appContext.LevelClass.FirstOrDefault(p => p.LevelClassId.Equals(new Guid(id)));
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }

        public Task<LevelClass> Get(Guid? index)
        {
            return this.appContext.LevelClass.FirstOrDefaultAsync(p => p.LevelClassId == index);
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
