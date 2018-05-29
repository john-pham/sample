// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using ebrain.admin.bc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Repositories.Interfaces
{
    public interface IGrpMaterialRepository : IRepository<GrpMaterial>
    {
        int Total { get; }
        IEnumerable<GrpMaterial> GetTopActive(int count);

        Task<IEnumerable<GrpMaterial>> Search(string filter, string value, string branchIds, bool isLearn, IEnumerable<Guid> typeLearnIds, int page, int size);
        Task<IEnumerable<GrpMaterial>> FindByTypeId(string typeid, string branchIds);
        Task<IEnumerable<GrpMaterial>> GrpMaterialsLearn(List<Guid> typeLearnIds, string branchIds);
        Task<IEnumerable<Guid>> GrpMaterialIdsLearn(List<Guid> typeLearnIds, string branchIds);
        Task<GrpMaterial> Save(GrpMaterial value, Guid? index);
        Task<GrpMaterial> FindById(Guid? id);
        Task<TypeMaterial> FindTypeByGrpId(Guid grpId);
        Task<Boolean> Delete(string id);
    }
}
