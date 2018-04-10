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
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        public UnitRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Unit> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<Unit> FindById(Guid? id)
        {
            return await this.appContext.Unit.FirstOrDefaultAsync(p => p.IsDeleted == false && p.UnitId == id);
        }

        public async Task<IEnumerable<Unit>> GetAllUnits(string branchIds)
        {
            return await this.appContext.Unit.Where(p => p.IsDeleted == false && branchIds.Contains(p.BranchId.ToString())).ToListAsync();
        }

        public async Task<IEnumerable<Unit>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.Unit.Where(p => p.IsDeleted == false
                            && branchIds.Contains(p.BranchId.ToString())).ToListAsync();
        }

        public async Task<Unit> Save(Unit value, Guid? id)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var item = await this.appContext.Unit.FirstOrDefaultAsync(p => p.IsDeleted == false && p.UnitId == id);
            if (item != null)
            {
                item.UnitCode = value.UnitCode;
                item.UnitName = value.UnitName;
                item.Note = value.Note;
            }
            else
            {
                var result = await appContext.Unit.AddAsync(value);
                item = result.Entity;
            }
            //
            await appContext.SaveChangesAsync();
            //
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

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
