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
    public class ClassStatusRepository : Repository<ClassStatus>, IClassStatusRepository
    {
        public ClassStatusRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<ClassStatus> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<ClassStatus>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.ClassStatus.Where(p => p.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<ClassStatus>> GetAll(string branchIds)
        {
            //dont usage branchIds.Contains(p.BranchId.ToString()
            return await this.appContext.ClassStatus.Where(
                p => p.IsDeleted == false
                ).ToListAsync();
        }

        public async Task<ClassStatus> Save(ClassStatus value)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var result = await appContext.ClassStatus.AddAsync(value);
            //
            await appContext.SaveChangesAsync();
            //
            return result.Entity;
        }

        public async Task<bool> Delete(string id)
        {
            var itemExist = appContext.ClassStatus.FirstOrDefault(p => p.ClassStatusId.Equals(new Guid(id)));
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
