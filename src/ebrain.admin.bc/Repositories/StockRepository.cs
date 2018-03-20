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
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        public StockRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Stock> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<Stock>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.Stock.Where(p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
        }

        public async Task<Stock> Save(Stock value, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var result = await appContext.Stock.AddAsync(value);
            //
            await appContext.SaveChangesAsync();
            //
            return result.Entity;
        }

        public async Task<bool> Delete(string id)
        {
            var itemExist = appContext.Stock.FirstOrDefault(p => p.StockId.Equals(new Guid(id)));
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }

        public async Task<Stock> Get(Guid index)
        {
            return await this.appContext.Stock.FirstOrDefaultAsync(p => p.StockId == index);
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
