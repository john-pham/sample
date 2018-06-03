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
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public int Total { get; private set; }
        public SupplierRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Supplier> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Supplier>> Search(string filter, string value, string branchIds, int isOption, int page, int size)
        {
            var results = this.appContext.Supplier.Where
                (
                    p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                );
            var grps = this.appContext.GrpSupplier.Where(p => p.IsDeleted == false).ToList();
            switch (isOption)
            {
                case 1://Khách hàng
                    var grpIds = grps.Where(p => p.IsCustomer == true).Select(p => p.GrpSupplierId).ToList();
                    results = results.Where(p => grpIds.Contains(p.GrpSupplierId.HasValue ? p.GrpSupplierId.Value : Guid.Empty));
                    break;
                case 2://NCC
                    grpIds = grps.Where(p => p.IsSupplier == true).Select(p => p.GrpSupplierId).ToList();
                    results = results.Where(p => grpIds.Contains(p.GrpSupplierId.HasValue ? p.GrpSupplierId.Value : Guid.Empty));
                    break;
                case 3://Nhân viên
                    grpIds = grps.Where(p => p.IsEmployee == true).Select(p => p.GrpSupplierId).ToList();
                    results = results.Where(p => grpIds.Contains(p.GrpSupplierId.HasValue ? p.GrpSupplierId.Value : Guid.Empty));
                    break;
                case 4://Teacher
                    grpIds = grps.Where(p => p.IsTeacher == true).Select(p => p.GrpSupplierId).ToList();
                    results = results.Where(p => grpIds.Contains(p.GrpSupplierId.HasValue ? p.GrpSupplierId.Value : Guid.Empty));
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

        public async Task<Supplier> Save(Supplier value, Guid? index)
        {
            try
            {
                value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
                var itemExist = await Get(index);
                if (itemExist != null)
                {
                    itemExist.SupplierCode = value.SupplierCode;
                    itemExist.SupplierName = value.SupplierName;
                    itemExist.TaxCode = value.TaxCode;
                    itemExist.Phone = value.Phone;
                    itemExist.Address = value.Address;
                    itemExist.Note = value.Note;
                    itemExist.GrpSupplierId = value.GrpSupplierId;
                    itemExist.UpdatedDate = DateTime.Now;
                    itemExist.UpdatedBy = value.UpdatedBy;
                    itemExist.Birthday = value.Birthday;
                    itemExist.UserLoginId = value.UserLoginId;
                }
                else
                {
                    var result = await appContext.Supplier.AddAsync(value);
                    itemExist = result.Entity;
                }
                await appContext.SaveChangesAsync();
                return itemExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(string id)
        {
            var itemExist = appContext.Supplier.FirstOrDefault(p => p.SupplierId.Equals(new Guid(id)));
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }

        public async Task<Supplier> Get(Guid? index)
        {
            return await this.appContext.Supplier.FirstOrDefaultAsync(p => p.SupplierId == index);
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
