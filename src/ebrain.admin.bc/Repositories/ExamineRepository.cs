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
    public class ExamineRepository : Repository<Examine>, IExamineRepository
    {
        public int Total { get; private set; }
        public ExamineRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Examine> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<Examine> FindById(Guid? id)
        {
            return await this.appContext.Examine.FirstOrDefaultAsync(p => p.IsDeleted == false && p.ExamineId == id);
        }

        public async Task<IEnumerable<Examine>> GetAllExamines(string branchIds)
        {
            return await this.appContext.Examine.Where(p => p.IsDeleted == false && branchIds.Contains(p.BranchId.ToString())).ToListAsync();
        }

        public async Task<IEnumerable<Examine>> Search(string filter, string value, string branchIds, int page, int size)
        {
            var someTypeList = this.appContext.Examine.Where(p => p.IsDeleted == false
                                                        && branchIds.Contains(p.BranchId.ToString()));

            //paging
            this.Total = someTypeList.Count();
            if (size > 0 && page >= 0)
            {
                someTypeList = (from c in someTypeList select c).Skip(page * size).Take(size);
            }
            return someTypeList;
        }

        public async Task<Examine> Save(Examine value, Guid? id)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var item = await this.appContext.Examine.FirstOrDefaultAsync(p => p.IsDeleted == false && p.ExamineId == id);
            if (item != null)
            {
                item.ExamineCode = value.ExamineCode;
                item.ExamineName = value.ExamineName;
                item.Note = value.Note;
            }
            else
            {
                var result = await appContext.Examine.AddAsync(value);
                item = result.Entity;
            }
            //
            await appContext.SaveChangesAsync();
            //
            return item;
        }

        public async Task<bool> Delete(string id)
        {
            //Check exist in materials
            var ExamineExist = this.appContext.ClassExamine.FirstOrDefault(p => p.ExamineId.Equals(new Guid(id)));
            if (ExamineExist != null) throw new Exception("Exist in ClassExamine");

            var itemExist = appContext.Examine.FirstOrDefault(p => p.ExamineId.Equals(new Guid(id)));
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
