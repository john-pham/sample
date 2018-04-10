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
    public interface IFeatureGroupRepository : IRepository<FeatureGroup>
    {
        int Total { get; }

        Task<FeatureGroup> Update(FeatureGroup value, Guid? index);

        Task<bool> Delete(Guid index);

        Task<IList<Report.FeatureGroup>> Search(string value, int page, int size);

        Task<Report.FeatureGroup> GetItem(Guid Index);
    }
}
