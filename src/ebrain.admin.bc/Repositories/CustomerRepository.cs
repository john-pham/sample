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
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Customer> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<Customer>> Search(string filter, string value)
        {
            return await (from c in this.appContext.Customers
                         where c.Name.Contains(value)
                         select c).ToListAsync();
        }

        public async Task<Customer> Save(Customer value)
        {
            var result = await appContext.Customers.AddAsync(value);
            //
            await appContext.SaveChangesAsync();
            //
            return result.Entity;
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
