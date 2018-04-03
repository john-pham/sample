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
    public interface IGroupDocumentRepository : IRepository<GroupDocument>
    {
        int Total { get; }
        IEnumerable<GroupDocument> GetTopActive(int count);
        Task<IEnumerable<GroupDocument>> Search(string filter, string value, int page, int size, string branchIds);
        Task<IEnumerable<GroupDocument>> GroupDocuments(string branchIds);
        Task<GroupDocument> Save(GroupDocument value, Guid? index);
        Task<GroupDocument> FindById(Guid? id);
        Task<Boolean> Delete(Guid? id);
    }
}
