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
    public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
    {
        public int Total { get; private set; }

        public AttendanceRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Attendance> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AttendanceList>> Search(string filterValue, string classId, string studentId, DateTime createDate, string branchIds, int page, int size)
        {
            try
            {
                List<AttendanceList> someTypeList = new List<AttendanceList>();
                this.appContext.LoadStoredProc("dbo.sp_Attendance")
                               .WithSqlParam("@filterValue", filterValue)
                               .WithSqlParam("@studentId", studentId)
                               .WithSqlParam("@classId", classId)
                               .WithSqlParam("@createDate", createDate)
                               .WithSqlParam("@BranchIds", branchIds)
                               .ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<AttendanceList>().ToList();
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

        public async Task<bool> Save(Attendance[] values, Guid createBy)
        {
            try
            {
                var branchId = createBy.GetBranchOfCurrentUser(this.appContext);
                foreach (var item in values)
                {
                    item.BranchId = branchId;
                    var itemExist = this.appContext.Attendance.FirstOrDefault(
                            p => p.AttendanceDate.Date == item.AttendanceDate.Date
                            && p.ClassId == item.ClassId
                            && p.StudentId == item.StudentId
                            );
                    if (itemExist != null)
                    {
                        itemExist.BranchId = branchId;
                        itemExist.Absent = item.Absent;
                        itemExist.UpdatedBy = createBy;
                        itemExist.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        await this.appContext.Attendance.AddAsync(item);
                    }
                }

                return await appContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(string id)
        {
            var itemExist = appContext.Attendance.FirstOrDefault(p => p.AttendanceId.Equals(new Guid(id)));
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }

        public Task<Attendance> Get(Guid? index)
        {
            return this.appContext.Attendance.FirstOrDefaultAsync(p => p.AttendanceId == index);
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
