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

namespace ebrain.admin.bc.Repositories
{
    public class IOStockDetailRepository : Repository<IOStockDetail>, IIOStockDetailRepository
    {
        public IOStockDetailRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<IOStockDetail> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IOStockDetail> FindById(Guid? id)
        {
            return await this.appContext.IOStockDetail.FirstOrDefaultAsync(p => p.IsDeleted == false && p.IOStockDetailId == id);
        }

        public async Task<IEnumerable<IOStockDetail>> GetAlls()
        {
            return await this.appContext.IOStockDetail.Where(p => p.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<IOStockDetail>> Search(string filter, string value)
        {
            return await this.appContext.IOStockDetail.Where(p => p.IsDeleted == false).ToListAsync();
        }

        public async Task<IOStockDetail> Save(IOStockDetail value, Guid? id)
        {
            var item = await this.appContext.IOStockDetail.FirstOrDefaultAsync(p => p.IsDeleted == false && p.IOStockDetailId == id);
            if (item != null)
            {
                
            }
            else
            {
                var result = await appContext.IOStockDetail.AddAsync(value);
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
