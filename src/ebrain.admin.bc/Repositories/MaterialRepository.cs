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
using ebrain.admin.bc.Report;
using ebrain.admin.bc.Utilities;

namespace ebrain.admin.bc.Repositories
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public MaterialRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Material> Get(Guid? index)
        {
            return await this.appContext.Material.FirstOrDefaultAsync(p => p.MaterialId == index);
        }

        public async Task<List<Tuple<Material, Unit[]>>> GetMaterialsAndUnits(int page, int pageSize, string branchIds)
        {
            IQueryable<Material> materialQuery = this.appContext.Material;
            IQueryable<Unit> unitQuery = this.appContext.Unit;

            if (page != -1)
                materialQuery = materialQuery.Skip((page - 1) * pageSize);

            if (pageSize != -1)
                materialQuery = materialQuery.Take(pageSize);

            var materials = await materialQuery.Where(p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();

            var units = await unitQuery.Where(p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();

            return materials.Select(u => Tuple.Create(u, units.ToArray())).ToList();
        }
        public async Task<IEnumerable<Material>> Search(string filter, string value, int? page, int? size, string branchIds)
        {
            return await this.appContext
                .Material
                .Where(p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                )
                //.Take(size)
                //.Skip(page)
                .ToListAsync();
        }

        public IEnumerable<MaterialList> GetAllMaterialList(int page, int size, string filterValue, string branchIds, bool isLearn)
        {
            try
            {
                List<MaterialList> someTypeList = new List<MaterialList>();
                this.appContext.LoadStoredProc("dbo.sp_MaterialList")
                               .WithSqlParam("filterValue", filterValue)
                               .WithSqlParam("branchIds", branchIds)
                               .WithSqlParam("isLearn", isLearn).ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<MaterialList>().ToList();
                               });

                return someTypeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<IEnumerable<Guid>> TypeLearnIds(string branchIds)
        {
            return await this.appContext.TypeMaterial.Where(p => p.IsLearning == true && p.IsDeleted == false)
                .Select(p => p.TypeMaterialId).ToListAsync();
        }

        private async Task<IEnumerable<Guid>> GrpMaterialIdLearnIds(string branchIds)
        {
            var itemTypeIds = TypeLearnIds(branchIds);
            if (itemTypeIds != null && itemTypeIds.Result != null)
            {
                return await this.appContext.GrpMaterial.Where
                    (p => itemTypeIds.Result.Contains(p.TypeMaterialId.HasValue ? p.TypeMaterialId.Value : new Guid())
                            && p.IsDeleted == false)
                    .Select(p => p.GrpMaterialId).ToListAsync();
            }
            return null;
        }

        public async Task<Material> Save(Material value, MaterialHead valueHead, Guid? id)
        {
            //get branchId
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            valueHead.BranchId = value.BranchId;

            var item = this.appContext.Material.FirstOrDefault(p => p.MaterialId == id);
            if (item != null)
            {
                item.GrpMaterialId = value.GrpMaterialId;
                item.MaterialCode = value.MaterialCode;
                item.MaterialName = value.MaterialName;
                item.Note = value.Note;
                item.UnitId = value.UnitId;

                var itemHead = this.appContext.MaterialHead.FirstOrDefault(p => p.MaterialId == id);
                itemHead.Price = valueHead.Price;
                itemHead.Note = valueHead.Note;
                itemHead.NumberHourse = valueHead.NumberHourse;
                itemHead.PriceAfterVAT = valueHead.PriceAfterVAT;
                itemHead.SellPrice = valueHead.SellPrice;
                itemHead.SpBeCourse = valueHead.SpBeCourse;
                itemHead.SpEnCourse = valueHead.SpEnCourse;

                itemHead.MaskPassCourse = valueHead.MaskPassCourse;
                itemHead.CalBeCourse = valueHead.CalBeCourse;
                itemHead.CalEnCourse = valueHead.CalEnCourse;
            }

            else
            {
                var result = await appContext.Material.AddAsync(value);
                await appContext.MaterialHead.AddAsync(valueHead);
                item = result.Entity;
            }
            await appContext.SaveChangesAsync();
            return item;
        }

        public async Task<MaterialHead> Save(MaterialHead value, Guid? index)
        {
            var result = await appContext.MaterialHead.AddAsync(value);
            //save material
            await appContext.SaveChangesAsync();
            //
            return result.Entity;
        }

        public async Task<MaterialHead> FindHeadByMaterialId(Guid guid)
        {
            return await this.appContext.MaterialHead.FirstOrDefaultAsync(p => p.MaterialId == guid);
        }

        public async Task<bool> Delete(string id)
        {
            var itemExist = appContext.Material.FirstOrDefault(p => p.MaterialId.Equals(new Guid(id)));
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
