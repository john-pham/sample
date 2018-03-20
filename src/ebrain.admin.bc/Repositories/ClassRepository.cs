﻿// ======================================
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
    public class ClassRepository : Repository<Class>, IClassRepository
    {
        public ClassRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Class> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<Class>> Search(string filter, string value, string branchIds)
        {
            return await this.appContext.Class.Where(p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                ).ToListAsync();
        }

        public async Task<Class> Save(Class value, ClassTime[] classTimes, ClassStudent[] classStudents, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var itemExist = await Get(index);
            if (itemExist != null)
            {
                itemExist.ClassCode = value.ClassCode;
                itemExist.ClassName = value.ClassName;
                itemExist.Note = value.Note;
                itemExist.UpdatedBy = value.UpdatedBy;
                itemExist.UpdatedDate = DateTime.Now;
                itemExist.BranchId = value.BranchId;
                itemExist.StartDate = value.StartDate;
                itemExist.EndDate = value.EndDate;
                itemExist.StatusId = value.StatusId;
                itemExist.SupplierId = value.SupplierId;
                itemExist.MaxStudent = value.MaxStudent;
                itemExist.MaterialId = value.MaterialId;
                itemExist.LongLearn = value.LongLearn;

                var cTimeExists = await GeTimeByClassId(index);
                //update deleted
                var iodIds = classTimes.Select(p => p.ClassTimeId);
                var iodNotExists = cTimeExists.Where(p => !iodIds.Contains(p.ClassTimeId));
                foreach (var itemDetail in iodNotExists)
                {
                    itemDetail.IsDeleted = true;
                }

                //set times
                foreach (var item in classTimes)
                {
                    var itemClassExist = await this.appContext.ClassTime.FirstOrDefaultAsync(p => p.ClassTimeId == item.ClassTimeId);
                    if (itemClassExist != null)
                    {
                        itemClassExist.BranchId = value.BranchId;
                        itemClassExist.EndTime = item.EndTime;
                        itemClassExist.StartTime = item.StartTime;
                        itemClassExist.SupplierId = item.SupplierId;
                        itemClassExist.RoomId = item.RoomId;
                        itemClassExist.OnTodayId = item.OnTodayId;
                        itemClassExist.ClassId = itemExist.ClassId;
                    }
                    else
                    {
                        item.ClassId = itemExist.ClassId;
                        await this.appContext.ClassTime.AddAsync(item);
                    }
                }

                var cStudentExists = await GeStudentByClassId(index);
                //update deleted
                var iodStIds = classStudents.Select(p => p.ClassStudentId);
                var iodStNotExists = cStudentExists.Where(p => !iodStIds.Contains(p.ClassStudentId));
                foreach (var itemDetail in iodStNotExists)
                {
                    itemDetail.IsDeleted = true;
                }

                //set students 
                foreach (var item in classStudents)
                {
                    var itemClassExist = await this.appContext.ClassStudent.FirstOrDefaultAsync(p => p.ClassStudentId == item.ClassStudentId);
                    if (itemClassExist != null)
                    {
                        itemClassExist.BranchId = value.BranchId;
                        itemClassExist.StudentId = item.StudentId;
                        itemClassExist.ClassId = itemExist.ClassId;
                    }
                    else
                    {
                        item.ClassId = itemExist.ClassId;
                        await this.appContext.ClassStudent.AddAsync(item);
                    }
                }
            }
            else
            {
                var result = await appContext.Class.AddAsync(value);
                var classId = result.Entity.ClassId;
                //Times
                foreach (var item in classTimes)
                {
                    item.ClassTimeId = Guid.NewGuid();
                    item.ClassId = classId;
                    item.BranchId = value.BranchId;
                }
                //Students
                foreach (var item in classStudents)
                {
                    item.ClassStudentId = Guid.NewGuid();
                    item.ClassId = classId;
                    item.BranchId = value.BranchId;
                }
                await appContext.ClassTime.AddRangeAsync(classTimes);
                await appContext.ClassStudent.AddRangeAsync(classStudents);
                itemExist = result.Entity;
            }

            await appContext.SaveChangesAsync();
            return itemExist;
        }

        private async Task<IEnumerable<ClassTime>> GeTimeByClassId(Guid? classId)
        {
            return await this.appContext.ClassTime.Where(p => p.IsDeleted == false && p.ClassId == classId).ToListAsync();
        }

        private async Task<IEnumerable<ClassStudent>> GeStudentByClassId(Guid? classId)
        {
            return await this.appContext.ClassStudent.Where(p => p.IsDeleted == false && p.ClassId == classId).ToListAsync();
        }

        public async Task<bool> Delete(string id)
        {
            var itemExist = appContext.Class.FirstOrDefault(p => p.ClassId.Equals(new Guid(id)));
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }

        public Task<Class> Get(Guid? index)
        {
            return this.appContext.Class.FirstOrDefaultAsync(p => p.ClassId == index);
        }

        public List<ClassList> GetClasses(string branchIds, string value, Guid? statusId, Guid? supplierId)
        {
            try
            {
                List<ClassList> someTypeList = new List<ClassList>();
                this.appContext.LoadStoredProc("dbo.sp_Classes")
                               .WithSqlParam("statusId", (statusId != null ? statusId.ToString() : null))
                               .WithSqlParam("supplierId", (supplierId != null ? supplierId.ToString() : null))
                               .WithSqlParam("filterValue", value)
                               .WithSqlParam("BranchIds", branchIds)
                               .ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<ClassList>().ToList();
                               });

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
