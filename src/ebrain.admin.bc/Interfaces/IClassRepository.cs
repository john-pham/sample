﻿// ======================================
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
    public interface IClassRepository : IRepository<Class>
    {
        IEnumerable<Class> GetTopActive(int count);

        Task<IEnumerable<Class>> Search(string filter, string value, string branchIds);

        Task<Class> Save(Class value, ClassTime[] classTimes, ClassStudent[] classStudents, Guid? index);
        Task<Boolean> Delete(string id);
        Task<Class> Get(Guid? index);
        List<ClassList> GetClasses(string branchIds, string value, Guid? statusId, Guid? supplierId);
    }
}
