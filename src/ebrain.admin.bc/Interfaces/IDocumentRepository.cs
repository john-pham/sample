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
    public interface IDocumentRepository : IRepository<Document>
    {
        int Total { get; }
        IEnumerable<Document> GetTopActive(int count);
        Task<IEnumerable<Document>> Search(string filter, string value, int page, int size, string branchIds);
        Task<IEnumerable<Document>> Documents(string branchIds);
        Task<Document> Save(Document value, Guid? index);
        Task<Document> FindById(Guid? id);
        Task<Boolean> Delete(Guid? id);
    }
}
