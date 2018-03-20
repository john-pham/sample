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
    public interface IConfigNumberOfCodeRepository : IRepository<ConfigNumberOfCode>
    {
        IEnumerable<ConfigNumberOfCode> GetTopActive(int count);

        Task<IEnumerable<ConfigNumberOfCode>> Search(string filter, string value);

        Task<ConfigNumberOfCode> Save(ConfigNumberOfCode value);
        Task<Boolean> Delete(string id);
        string GenerateCode(int iotypeId, string empployId);
        string GenerateCodePayment(int paymentTypeId, string empployId);
        string GenerateCodeMain(string key, string code, string empId, int numberTable);
    }
}
