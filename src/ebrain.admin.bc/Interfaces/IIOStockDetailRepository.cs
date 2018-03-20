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
    public interface IIOStockDetailRepository : IRepository<IOStockDetail>
    {
        IEnumerable<IOStockDetail> GetTopActive(int count);
        Task<IOStockDetail> FindById(Guid? id);
        Task<IEnumerable<IOStockDetail>> Search(string filter, string value);
        Task<IEnumerable<IOStockDetail>> GetAlls();
        Task<IOStockDetail> Save(IOStockDetail value, Guid? id);
        Task<Boolean> Delete(string id);
    }
}
