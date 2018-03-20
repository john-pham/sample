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
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Material> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.Student.Where(p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
        }
        public async Task<IEnumerable<Student>> GetAll(int page, int pageSize, string branchIds)
        {
            return await this.appContext.Student.Where(p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
        }
       
        public async Task<Student> Get(Guid? index)
        {
            return await this.appContext.Student.FirstOrDefaultAsync(p => p.StudentId == index);
        }
        public async Task<Student> Save(Student value, StudentRelationShip valueRelationShip, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var itemExist = await Get(index);
            if (itemExist == null)
            {
                var result = await appContext.Student.AddAsync(value);
                await appContext.StudentRelationShip.AddAsync(valueRelationShip);
                await appContext.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                itemExist.StudentCode = value.StudentCode;
                
                itemExist.StudentName = value.StudentName;
                itemExist.AccountBank = value.AccountBank;
                itemExist.Address = value.Address;
                itemExist.Birthday = DateTime.Now;//value.Birthday,
                itemExist.ClassName = value.ClassName;
                itemExist.SchoolName = value.SchoolName;
                itemExist.Phone = value.Phone;
                itemExist.TaxCode = value.TaxCode;
                itemExist.Email = value.Email;
                itemExist.GenderId = value.GenderId;
                itemExist.UserName = value.UserName;
                itemExist.Password = value.Password;
                itemExist.Note = value.Note;

                var itemRelation = await FindRelationShipByStudentId(itemExist.StudentId);
                if (itemRelation == null)
                {
                    await appContext.StudentRelationShip.AddAsync(valueRelationShip);
                }
                else
                {
                    itemRelation.StudentId = itemExist.StudentId;
                    itemRelation.FullName = valueRelationShip.FullName;
                    itemRelation.Facebook = valueRelationShip.Facebook;
                    itemRelation.Address = valueRelationShip.Address;
                    itemRelation.Email = valueRelationShip.Email;
                    itemRelation.Job = valueRelationShip.Job;
                    itemRelation.Phone = valueRelationShip.Phone;
                    itemRelation.Birthday = DateTime.Now;//value.Birthday,
                    itemRelation.BranchId = Guid.NewGuid();
                    itemRelation.RelationRequire = valueRelationShip.RelationRequire;
                    itemRelation.CreatedDate = DateTime.Now;
                    itemRelation.UpdatedDate = DateTime.Now;

                    itemRelation.UpdatedBy = valueRelationShip.UpdatedBy;
                }

                await appContext.SaveChangesAsync();
                return itemExist;
            }

        }

        public async Task<StudentRelationShip> FindRelationShipByStudentId(Guid guid)
        {
            return await this.appContext.StudentRelationShip.FirstOrDefaultAsync(p => p.StudentId == guid);
        }

        public async Task<bool> Delete(string id)
        {
            var itemExist = appContext.Student.FirstOrDefault(p => p.StudentId.Equals(new Guid(id)));
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
