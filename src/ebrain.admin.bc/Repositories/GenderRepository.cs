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
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        public GenderRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Gender> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<Gender> FindById(long? id)
        {
            return await this.appContext.Gender.FirstOrDefaultAsync(p => p.IsDeleted == false && p.GenderId == id);
        }

        public async Task<IEnumerable<Gender>> GetAllGender()
        {
            try
            {
                return await this.appContext.Gender.Where(p => p.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Gender>> Search(string filter, string value)
        {
            return await this.appContext.Gender.Where(p => p.IsDeleted == false).ToListAsync();
        }

        public async Task<Gender> Save(Gender value, long? id)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var item = await this.appContext.Gender.FirstOrDefaultAsync(p => p.IsDeleted == false && p.GenderId == id);
            if (item != null)
            {
                item.GenderCode = value.GenderCode;
                item.GenderName = value.GenderName;
                item.Note = value.Note;
            }
            else
            {
                var result = await appContext.Gender.AddAsync(value);
                item = result.Entity;
            }
            //
            await appContext.SaveChangesAsync();
            //
            return item;
        }

        public async Task<bool> Delete(long? id)
        {
            
            return true;
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
