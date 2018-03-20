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
    public interface IClassStatusRepository : IRepository<ClassStatus>
    {
        IEnumerable<ClassStatus> GetTopActive(int count);

        Task<IEnumerable<ClassStatus>> Search(string filter, string value, string branchIds);

        Task<ClassStatus> Save(ClassStatus value);
        Task<Boolean> Delete(string id);
        Task<IEnumerable<ClassStatus>> GetAll(string branchIds);
    }
}
