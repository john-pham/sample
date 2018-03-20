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
    public interface IGenderRepository : IRepository<Gender>
    {
        IEnumerable<Gender> GetTopActive(int count);
        Task<Gender> FindById(long? id);
        Task<IEnumerable<Gender>> Search(string filter, string value);
        Task<IEnumerable<Gender>> GetAllGender();
        Task<Gender> Save(Gender value, long? id);
        Task<Boolean> Delete(long? id);
    }
}
