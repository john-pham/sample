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
    public interface IAccessRightsRepository : IRepository<AccessRight>
    {
        int Total { get; }

        Task<bool> Update(IEnumerable<AccessRight> values);

        Task<bool> Delete(Guid feature, Guid group);

        Task<IList<Report.AccessRight>> Search(Guid groupId, Guid? featureGroupId, int page, int size);

        Task<Report.AccessRight> GetItem(Guid featureId, Guid Id);
    }
}
