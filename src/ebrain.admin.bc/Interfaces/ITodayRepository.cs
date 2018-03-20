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
    public interface ITodayRepository : IRepository<Today>
    {
        IEnumerable<Today> GetTopActive(int count);

        Task<IEnumerable<Today>> Search(string filter, string value, string branchIds);

        Task<Today> Save(Today value);
        Task<Boolean> Delete(string id);
        Task<IEnumerable<Today>> GetAll(string branchIds);
    }
}
