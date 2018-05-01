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
    public class GrpSupplierRepository : Repository<GrpSupplier>, IGrpSupplierRepository
    {
        public int Total { get; private set; }
        public GrpSupplierRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<GrpSupplier> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<GrpSupplier>> Search(string filter, string value, string branchIds, int page, int size)
        {
            var results =  this.appContext.GrpSupplier.Where(p => p.IsDeleted == false && branchIds.Contains(p.BranchId.ToString()));

            //paging
            this.Total = results.Count();
            if (size > 0 && page >= 0)
            {
                results = (from c in results select c).Skip(page * size).Take(size);
            }
            return results;
        }

        public async Task<IEnumerable<GrpSupplier>> GetAll(string branchIds, int option, int page, int size)
        {
            var results = this.appContext.GrpSupplier.Where(p => p.IsDeleted == false && branchIds.Contains(p.BranchId.ToString()));
            switch (option)
            {
                case 1://customer
                    results = results.Where(p => p.IsCustomer);
                    break;
                case 2://supplier
                    results = results.Where(p => p.IsSupplier);
                    break;
                case 3://Employee
                    results = results.Where(p => p.IsEmployee);
                    break;
                case 4://Employee
                    results = results.Where(p => p.IsTeacher);
                    break;
            }
            //paging
            this.Total = results.Count();
            if (size > 0 && page >= 0)
            {
                results = (from c in results select c).Skip(page * size).Take(size);
            }
            return results;
        }

        public async Task<GrpSupplier> Save(GrpSupplier value, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var itemExist = await Get(index);
            if (itemExist != null)
            {
                itemExist.GrpSupplierCode = value.GrpSupplierCode;
                itemExist.GrpSupplierName = value.GrpSupplierName;
                itemExist.Note = value.Note;
                itemExist.UpdatedBy = value.UpdatedBy;
                itemExist.UpdatedDate = DateTime.Now;
                itemExist.IsCustomer = value.IsCustomer;
                itemExist.IsEmployee = value.IsEmployee;
                itemExist.IsSupplier = value.IsSupplier;
                itemExist.IsTeacher = value.IsTeacher;
            }
            else
            {
                var result = await appContext.GrpSupplier.AddAsync(value);
                itemExist = result.Entity;
            }

            await appContext.SaveChangesAsync();
            return itemExist;
        }

        public async Task<bool> Delete(string id)
        {
            //Check exist in materials
            var grpExist = this.appContext.Supplier.FirstOrDefault(p => p.GrpSupplierId.Equals(new Guid(id)));
            if (grpExist != null) throw new Exception("Exist in Supplier");

            var itemExist = appContext.GrpSupplier.FirstOrDefault(p => p.GrpSupplierId.Equals(new Guid(id)));
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }
        public async Task<GrpSupplier> Get(Guid? index)
        {
            return await this.appContext.GrpSupplier.FirstOrDefaultAsync(p => p.GrpSupplierId == index);
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
