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
    public class GrpMaterialRepository : Repository<GrpMaterial>, IGrpMaterialRepository
    {
        public GrpMaterialRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<GrpMaterial> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GrpMaterial>> GrpMaterialsLearn(List<Guid> typeLearnIds, string branchIds)
        {
            return await this.appContext.GrpMaterial.Where(p =>
                            typeLearnIds.Contains
                            (p.TypeMaterialId.HasValue ? p.TypeMaterialId.Value : new Guid())
                            && branchIds.Contains(p.BranchId.ToString())
                            ).ToListAsync();
        }
        public async Task<GrpMaterial> FindById(Guid? id)
        {
            return await this.appContext.GrpMaterial.FirstOrDefaultAsync(p => p.GrpMaterialId == id);
        }
        public async Task<TypeMaterial> FindTypeByTypeId(Guid? typeId)
        {
            return await this.appContext.TypeMaterial.FirstOrDefaultAsync(p => p.TypeMaterialId == typeId);
        }
        public async Task<TypeMaterial> FindTypeByGrpId(Guid grpId)
        {
            var itemGrp = await FindById(grpId);
            if (itemGrp != null)
            {
                return await FindTypeByTypeId(itemGrp.TypeMaterialId);
            }
            return null;
        }
        public async Task<IEnumerable<GrpMaterial>> FindByTypeId(string typeid, string branchIds)
        {
            return await this.appContext.GrpMaterial.Where(p => p.TypeMaterialId != null &&
                    p.TypeMaterialId.Equals(new Guid(typeid)) &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
        }
        public async Task<IEnumerable<Guid>> GrpMaterialIdsLearn(List<Guid> typeLearnIds, string branchIds)
        {
            var itemExists = await GrpMaterialsLearn(typeLearnIds, branchIds);
            return itemExists.Select(p => p.GrpMaterialId).ToList();
        }

        public async Task<IEnumerable<GrpMaterial>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.GrpMaterial.Where
                (
                    p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
        }

        public async Task<GrpMaterial> Save(GrpMaterial value, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var itemExist = await FindById(index);
            if (itemExist != null)
            {
                itemExist.GrpMaterialCode = value.GrpMaterialCode;
                itemExist.GrpMaterialName = value.GrpMaterialName;
                itemExist.TypeMaterialId = value.TypeMaterialId;
                itemExist.Note = value.Note;
                itemExist.UpdatedBy = value.UpdatedBy;
                itemExist.UpdatedDate = DateTime.Now;
            }
            else
            {
                var result = await appContext.GrpMaterial.AddAsync(value);
                itemExist = result.Entity;
            }
            await appContext.SaveChangesAsync();
            //
            return itemExist;
        }

        public async Task<bool> Delete(string id)
        {
            //Check exist in materials
            var grpExist = this.appContext.Material.FirstOrDefault(p => p.GrpMaterialId.Equals(new Guid(id)));
            if (grpExist != null) throw new Exception("Exist in materials");

            var itemExist = appContext.GrpMaterial.FirstOrDefault(p => p.GrpMaterialId.Equals(new Guid(id)));
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
