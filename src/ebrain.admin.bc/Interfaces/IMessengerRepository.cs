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
    public interface IMessengerRepository : IRepository<Messenger>
    {
        int Total { get; }
        Task<Messenger> Get(Guid? index, Guid userId);
        IEnumerable<MessengerReport> Search(Guid? userIdLogin, string filter, string value, string branchIds, int page, int size);
        Task<Messenger> Save(Messenger value, MessengerBranch[] messageBranchs, Guid? index);
        Task<Boolean> Delete(Guid id);
    }
}
