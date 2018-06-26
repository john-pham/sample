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
using ebrain.admin.bc.Report;

namespace ebrain.admin.bc.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public int Total { get; private set; }
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
                itemExist.Birthday = value.Birthday;
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
                    itemRelation.Birthday = value.Birthday;
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

        public List<StudentList> GetStudentBirthday(string branchIds, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                List<StudentList> someTypeList = new List<StudentList>();
                this.appContext.LoadStoredProc("dbo.sp_StudentBirthday")
                               .WithSqlParam("@fromDate", fromDate)
                               .WithSqlParam("@toDate", toDate)
                               .WithSqlParam("@BranchIds", branchIds)
                               .ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<StudentList>().ToList();
                               });

                return someTypeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StudentList> GetStudentEndClass(string branchIds, string classId, DateTime? today)
        {
            try
            {
                List<StudentList> someTypeList = new List<StudentList>();
                this.appContext.LoadStoredProc("dbo.sp_ClassStudent_End")
                               .WithSqlParam("@classId", classId)
                               .WithSqlParam("@toDay", today)
                               .WithSqlParam("@BranchIds", branchIds)
                               .ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<StudentList>().ToList();
                               });
                this.Total = someTypeList.Count;
                return someTypeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StudentList> GetStudentByCreateDate(string branchIds, DateTime? fromDate, DateTime? toDate, int page, int size)
        {
            try
            {
                var fr = DateTime.Now;
                var to = DateTime.Now;
                if (fromDate.HasValue)
                {
                    fr = fromDate.Value.Date;
                }
                if (toDate.HasValue)
                {
                    to = new DateTime(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day, 23, 59, 59);
                }

                List<StudentList> someTypeList = new List<StudentList>();
                this.appContext.LoadStoredProc("dbo.sp_Students")
                               .WithSqlParam("@fromDate", fromDate)
                               .WithSqlParam("@toDate", toDate)
                               .WithSqlParam("@BranchIds", branchIds)
                               .ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<StudentList>().ToList();
                               });

                //paging
                this.Total = someTypeList.Count();
                if (size > 0 && page >= 0)
                {
                    someTypeList = (from c in someTypeList select c).Skip(page * size).Take(size).ToList();
                }
                return someTypeList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StudentList> GetStudentCourse(string filterValue, string studentId, string branchIds, int page, int size)
        {
            try
            {
                List<StudentList> someTypeList = new List<StudentList>();
                this.appContext.LoadStoredProc("dbo.sp_Students_Course")
                               .WithSqlParam("@filterValue", filterValue)
                               .WithSqlParam("@studentId", studentId)
                               .WithSqlParam("@branchIds", branchIds)
                               .ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<StudentList>().ToList();
                               });

                //paging
                this.Total = someTypeList.Count();
                if (size > 0 && page >= 0)
                {
                    someTypeList = (from c in someTypeList select c).Skip(page * size).Take(size).ToList();
                }
                return someTypeList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StudentList> GetTeacherCourse(string filterValue, string branchIds, int page, int size)
        {
            try
            {
                List<StudentList> someTypeList = new List<StudentList>();
                this.appContext.LoadStoredProc("dbo.sp_ClassStudent_Teacher")
                               .WithSqlParam("@filterValue", filterValue)
                               .WithSqlParam("@branchIds", branchIds)
                               .ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<StudentList>().ToList();
                               });

                //paging
                this.Total = someTypeList.Count();
                if (size > 0 && page >= 0)
                {
                    someTypeList = (from c in someTypeList select c).Skip(page * size).Take(size).ToList();
                }
                return someTypeList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StudentList> GetStudentPotential(string filterValue, string branchIds, int page, int size)
        {
            try
            {
                List<StudentList> someTypeList = new List<StudentList>();
                this.appContext.LoadStoredProc("dbo.sp_Students_Potential")
                               .WithSqlParam("@filterValue", filterValue)
                               .WithSqlParam("@branchIds", branchIds)
                               .ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<StudentList>().ToList();
                               });

                //paging
                this.Total = someTypeList.Count();
                if (size > 0 && page >= 0)
                {
                    someTypeList = (from c in someTypeList select c).Skip(page * size).Take(size).ToList();
                }
                return someTypeList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StudentList> GetStudentLearning(string filterValue, Guid? studentId, Guid? classId, bool? isLearning, string branchIds, int page, int size)
        {
            try
            {
                List<StudentList> someTypeList = new List<StudentList>();
                this.appContext.LoadStoredProc("dbo.sp_Students_Learning")
                               .WithSqlParam("@filterValue", filterValue)
                               .WithSqlParam("@studentId", studentId != null ? studentId.ToString() : null)
                               .WithSqlParam("@classId", classId != null ? classId.ToString() : null)
                               .WithSqlParam("@branchIds", branchIds)
                               .ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<StudentList>().ToList();
                               });

                if (isLearning == true)
                {
                    someTypeList = someTypeList.Where(p => p.EndDate.Value.Date > DateTime.Now.Date).ToList();
                }
                else if (isLearning == false)
                {
                    someTypeList = someTypeList.Where(p => p.EndDate.Value.Date <= DateTime.Now.Date).ToList();
                }

                //paging
                this.Total = someTypeList.Count();
                if (size > 0 && page >= 0)
                {
                    someTypeList = (from c in someTypeList select c).Skip(page * size).Take(size).ToList();
                }
                return someTypeList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
