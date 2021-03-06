﻿// ======================================
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
    public interface IFeatureNoResultRepository : IRepository<FeatureNoResult>
    {
        void SenderEmailTemplate(Guid? featureId, Guid createdBy);
        void Save(FeatureNoResult value);

    }
}
