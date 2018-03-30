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
    public interface ISMSRepository : IRepository<SMS>
    {
        int Total { get; }

        Task<SMS> Get(Guid? index);

        Task<IEnumerable<SMS>> Search(string filter, string value, int page, int size, string branchIds);

        Task<SMS> Save(SMS value, Guid? oldId);

        Task<Boolean> Delete(Guid id);
        Task<string> SendSMS(Guid userId, string phone, string body);
    }
}
