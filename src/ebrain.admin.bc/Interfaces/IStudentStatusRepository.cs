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
    public interface IStudentStatusRepository : IRepository<StudentStatus>
    {
        int Total { get; }
        IEnumerable<StudentStatus> GetTopActive(int count);
        Task<StudentStatus> FindById(Guid? id);
        Task<IEnumerable<StudentStatus>> Search(string filter, string value, string branchIds);
        Task<IEnumerable<StudentStatus>> GetAllStudentStatus(string branchIds);
        Task<StudentStatus> Save(StudentStatus value, Guid? id);
        Task<Boolean> Delete(string id);
    }
}
