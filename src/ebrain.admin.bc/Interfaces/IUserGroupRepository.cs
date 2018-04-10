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
    public interface IUserGroupRepository : IRepository<UserGroup>
    {
        int Total { get; }

        Task<UserGroup> Update(UserGroup value, Guid? index);

        Task<bool> Delete(Guid index);

        Task<IList<Report.UserGroup>> Search(string value, int page, int size);

        Task<Report.UserGroup> GetItem(Guid Index);
    }
}
