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
    public interface ILevelClassRepository : IRepository<LevelClass>
    {
        int Total { get; }
        IEnumerable<LevelClass> GetTopActive(int count, string branchIds);

        Task<IEnumerable<LevelClass>> Search(string filter, string value, string branchIds);
        Task<LevelClass> Get(Guid? index);
        Task<LevelClass> Save(LevelClass value, Guid? index);
        Task<Boolean> Delete(string id);
    }
}
