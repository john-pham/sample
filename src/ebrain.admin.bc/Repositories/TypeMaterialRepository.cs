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
    public class TypeMaterialRepository : Repository<TypeMaterial>, ITypeMaterialRepository
    {
        public TypeMaterialRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<TypeMaterial> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<TypeMaterial> FindById(Guid? id)
        {
            return this.appContext.TypeMaterial.FirstOrDefault(p => p.TypeMaterialId == id);
        }

        public string FindNameById(Guid? id)
        {
            var item = FindById(id);
            if (item != null)
            {

            }
            return item != null && item.Result != null ? item.Result.TypeMaterialName : string.Empty;
        }
        public async Task<IEnumerable<TypeMaterial>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.TypeMaterial.Where(p => p.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<TypeMaterial>> GetAllTypeLearn(string branchIds)
        {
            return await this.appContext.TypeMaterial.Where(p => p.IsDeleted == false && p.IsLearning == true).ToListAsync();
        }

        public async Task<IEnumerable<TypeMaterial>> SearchLearn(string filter, string value, string branchIds)
        {
            return await this.appContext.TypeMaterial.Where(p => p.IsDeleted == false && p.IsLearning == true).ToListAsync();
        }

        public async Task<IEnumerable<Guid>> TypeIdsLearn(string branchIds)
        {
            return await this.appContext.TypeMaterial.Where(p => p.IsDeleted == false && p.IsLearning == true).Select(p => p.TypeMaterialId).ToListAsync();
        }

        public async Task<TypeMaterial> Save(TypeMaterial value, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var item = this.appContext.TypeMaterial.FirstOrDefault(p => p.TypeMaterialId == index);
            if (item != null)
            {
                item.TypeMaterialCode = value.TypeMaterialCode;
                item.TypeMaterialName = value.TypeMaterialName;
                item.Note = value.Note;
                item.IsDocument = value.IsDocument;
                item.IsLearning = value.IsLearning;
            }
            else
            {
                var result = await appContext.TypeMaterial.AddAsync(value);
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
            var typeExist = this.appContext.GrpMaterial.FirstOrDefault(p => p.TypeMaterialId.Equals(new Guid(id)));
            if (typeExist != null) throw new Exception("Exist in materials");

            var itemExist = appContext.TypeMaterial.FirstOrDefault(p => p.TypeMaterialId.Equals(new Guid(id)));
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
