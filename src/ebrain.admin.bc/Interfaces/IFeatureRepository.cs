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
    public interface IFeatureRepository : IRepository<Feature>
    {
        int Total { get; }

        Task<Feature> Update(Feature value);

        Task<bool> Delete(Guid index);

        Task<IList<Report.Feature>> Search(string value, int page, int size);

        Task<Report.Feature> GetItem(Guid Index);
        Task<FeatureNotification> GetFeatureNotification(Guid userId, Guid featureId);
        Task<bool> SaveFeatureNotification(FeatureNotification feature);
    }
}
