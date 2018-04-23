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
    public interface IAccessRightPersonsRepository : IRepository<AccessRightPerson>
    {
        int Total { get; }

        Task<bool> Update(IEnumerable<AccessRightPerson> values);

        Task<bool> Delete(Guid feature, Guid userId);

        Task<IList<Report.AccessRightPerson>> Search(Guid? userId, Guid? featureGroupId, string filterValue, string branchIds);
        Task<Guid?> GetUserIdFromAccessRightPerson(Guid? featureId, Guid? userId);
    }
}
