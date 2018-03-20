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
    public class PaymentTypeRepository : Repository<PaymentType>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<PaymentType> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<PaymentType>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.PaymentType.Where(p => p.IsDeleted == false).ToListAsync();
        }

        public async Task<PaymentType> Save(PaymentType value)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var result = await appContext.PaymentType.AddAsync(value);
            //
            await appContext.SaveChangesAsync();
            //
            return result.Entity;
        }

        public async Task<bool> Delete(string id)
        {
            var itemExist = appContext.PaymentType.FirstOrDefault(p => p.PaymentTypeId.Equals(new Guid(id)));
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
