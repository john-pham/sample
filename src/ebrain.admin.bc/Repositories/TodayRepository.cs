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
    public class TodayRepository : Repository<Today>, ITodayRepository
    {
        public TodayRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Today> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Today>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.Today.Where(p => p.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Today>> GetAll(string branchIds)
        {
            return await this.appContext.Today.Where(p => p.IsDeleted == false).ToListAsync();
        }

        public async Task<Today> Save(Today value)
        {
            var result = await appContext.Today.AddAsync(value);
            //
            await appContext.SaveChangesAsync();
            //
            return result.Entity;
        }

        public async Task<bool> Delete(string id)
        {
            var itemExist = appContext.Today.FirstOrDefault(p => p.TodayId.Equals(new Guid(id)));
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
