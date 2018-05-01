// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using ebrain.admin.bc.Models;
using ebrain.admin.bc.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Repositories.Interfaces
{
    public interface IMaterialRepository : IRepository<Material>
    {
        int Total { get; }
        Task<Material> Get(Guid? index);

        Task<List<Tuple<Material, Unit[]>>> GetMaterialsAndUnits(int page, int pageSize, string branchIds);

        Task<IEnumerable<Material>> Search(string filter, string value, int page, int size, string branchIds);

        Task<MaterialHead> FindHeadByMaterialId(Guid guid);

        Task<Material> Save(Material value, MaterialHead valueHead, Guid? id);

        Task<MaterialHead> Save(MaterialHead value, Guid? index);

        Task<Boolean> Delete(string id);
        IEnumerable<MaterialList> GetAllMaterialList(int page, int size,string filterValue, string branchIds, bool isLearn);
    }
}
