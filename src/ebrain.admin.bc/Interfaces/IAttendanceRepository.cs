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
    public interface IAttendanceRepository : IRepository<Attendance>
    {
        int Total { get; }
        IEnumerable<Attendance> GetTopActive(int count);
        Task<IEnumerable<AttendanceList>> Search(string filterValue, string classId, string studentId, DateTime createDate, string branchIds, int page, int size);
        Task<Attendance> Get(Guid? index);
        Task<bool> Save(Attendance[] values, Guid createBy);
        Task<Boolean> Delete(string id);
    }
}
