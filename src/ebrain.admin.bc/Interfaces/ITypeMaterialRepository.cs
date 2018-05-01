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
    public interface ITypeMaterialRepository : IRepository<TypeMaterial>
    {
        int Total { get; }
        IEnumerable<TypeMaterial> GetTopActive(int count);
        Task<TypeMaterial> FindById(Guid? id);
        string FindNameById(Guid? id);
        Task<IEnumerable<TypeMaterial>> GetAllTypeLearn(string branchIds);
        Task<IEnumerable<TypeMaterial>> Search(string filter, string value, string branchIds, int page, int size);
        Task<IEnumerable<TypeMaterial>> SearchLearn(string filter, string value, string branchIds, int page, int size);
        Task<IEnumerable<Guid>> TypeIdsLearn(string branchIds);
        Task<TypeMaterial> Save(TypeMaterial value, Guid? index);
        Task<Boolean> Delete(string id);
    }
}
