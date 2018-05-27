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
    public interface IStudentRepository : IRepository<Student>
    {
        int Total { get; }
        Task<IEnumerable<Student>> GetAll(int page, int pageSize, string branchIds);
        Task<IEnumerable<Student>> Search(string filter, string value, string branchIds);
        Task<StudentRelationShip> FindRelationShipByStudentId(Guid guid);
        Task<Student> Get(Guid? index);
        Task<Student> Save(Student value, StudentRelationShip valueReltion, Guid? index);
        Task<Boolean> Delete(string id);
        List<StudentList> GetStudentBirthday(string branchIds, DateTime? fromDate, DateTime? toDate);
        List<StudentList> GetStudentEndClass(string branchIds, string classId, DateTime? today);
        List<StudentList> GetStudentByCreateDate(string branchIds, DateTime? fromDate, DateTime? toDate, int page, int size);
        List<StudentList> GetStudentCourse(string filterValue, string studentId, string branchIds, int page, int size);
    }
}
