// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ebrain.admin.bc.Models;
using ebrain.admin.bc.Repositories.Interfaces;
using ebrain.admin.bc.Utilities;

namespace ebrain.admin.bc.Repositories
{
    public class ClassTimeRepository : Repository<ClassTime>, IClassTimeRepository
    {
        public ClassTimeRepository(ApplicationDbContext context) : base(context)
        { }


        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public async Task<IEnumerable<ClassTime>> GetClassTimeFromClassId(Guid? classId)
        {
            return await this.appContext.ClassTime.Where(p => p.ClassId == classId && p.IsDeleted == false).ToListAsync();
        }
    }
}
