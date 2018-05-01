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
    public class StudentStatusRepository : Repository<StudentStatus>, IStudentStatusRepository
    {
        public int Total { get; private set; }
        public StudentStatusRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<StudentStatus> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentStatus> FindById(Guid? id)
        {
            return await this.appContext.StudentStatus.FirstOrDefaultAsync(p => p.IsDeleted == false && p.StudentStatusId == id);
        }

        public async Task<IEnumerable<StudentStatus>> GetAllStudentStatus(string branchIds)
        {
            try
            {
                return await this.appContext.StudentStatus.Where(p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<StudentStatus>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.StudentStatus.Where(p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
        }

        public async Task<StudentStatus> Save(StudentStatus value, Guid? id)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var item = await this.appContext.StudentStatus.FirstOrDefaultAsync(p => p.IsDeleted == false && p.StudentStatusId == id);
            if (item != null)
            {
                item.StudentStatusCode = value.StudentStatusCode;
                item.StudentStatusName = value.StudentStatusName;
                item.Note = value.Note;
            }
            else
            {
                var result = await appContext.StudentStatus.AddAsync(value);
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
            var StudentStatusExist = this.appContext.Student.FirstOrDefault(p => p.StudentStatusId.Equals(new Guid(id)));
            if (StudentStatusExist != null) throw new Exception("Exist in Student");

            var itemExist = appContext.StudentStatus.FirstOrDefault(p => p.StudentStatusId.Equals(new Guid(id)));
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
