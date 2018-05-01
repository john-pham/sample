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
    public interface IRoomRepository : IRepository<Room>
    {
        int Total { get; }
        IEnumerable<Room> GetTopActive(int count);

        Task<IEnumerable<Room>> Search(string filter, string value, string branchIds, int page, int size);
        Task<Room> Get(Guid? index);
        Task<Room> Save(Room value, Guid? index);
        Task<Boolean> Delete(string id);
    }
}
