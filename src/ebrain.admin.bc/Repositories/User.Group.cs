using ebrain.admin.bc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ebrain.admin.bc.Utilities;
/*
 * Author:          John Pham
 * CreatedDate Date:    07/23/2013
 * 
 */
namespace ebrain.admin.bc.Repositories
{
    public class UserGroupRepository : Repository<UserGroup>, Interfaces.IUserGroupRepository
    {
        public int Total { get; private set; }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public UserGroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<UserGroup> Update(UserGroup value, Guid? index)
        {
            var m_Ret = default(UserGroup);

            if (value != null)
            {
                var cus = await appContext.UserGroup.FirstOrDefaultAsync(x => x.ID == index);
                value.BranchId = value.CreatedBy.HasValue ? value.CreatedBy.Value.GetBranchOfCurrentUser(this.appContext) : Guid.Empty;
                if (cus == null)
                {
                    var result = await appContext.UserGroup.AddAsync(value);
                    cus = result.Entity;
                }
                else
                {
                    cus.BranchId = value.BranchId;
                    cus.Name = value.Name;
                    cus.Description = value.Description;
                    cus.UpdatedDate = DateTime.Now;
                    cus.UpdatedBy = value.UpdatedBy;
                }
                //
                if (await appContext.SaveChangesAsync() > 0)
                {
                    m_Ret = cus;
                }
            }

            return m_Ret;

        }

        public async Task<bool> Delete(Guid index)
        {
            var m_Ret = false;

            var item = await appContext.UserGroup.FirstOrDefaultAsync(x => x.ID == index);

            if (item != null && appContext.Users.Count(x => x.GroupId == index) <= 0)
            {
                appContext.AccessRight.RemoveRange(appContext.AccessRight.Where(x => x.GroupID == index));
                appContext.UserGroup.Remove(item);
                //
                if (m_Ret = await appContext.SaveChangesAsync() > 0)
                {
                }
            }

            return m_Ret;
        }

        public async Task<IList<Report.UserGroup>> Search(string value, string branchIds, int page, int size)
        {
            var m_Ret = new List<Report.UserGroup>();

            var items = from c in appContext.UserGroup
                        where branchIds.Contains(c.BranchId.ToString())
                        select c;

            //FILTER
            if (!string.IsNullOrEmpty(value))
            {
                items = items.Where(x => x.Code == value ||
                        x.Name.Contains(value) ||
                        x.Description.Contains(value));
            }

            this.Total = await items.CountAsync();
            //
            if (size > 0 && page >= 0)
            {
                items = (from c in items
                         orderby c.CreatedDate descending
                         select c).Skip(page * size).Take(size);
            }

            foreach (var item in items)
            {
                m_Ret.Add(new Report.UserGroup
                {
                    ID = item.ID,
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description
                });
            }

            return m_Ret;
        }

        public async Task<Report.UserGroup> GetItem(Guid index)
        {
            var m_Ret = default(Report.UserGroup);

            var item = await appContext.UserGroup.FirstOrDefaultAsync(x => (x.ID == index));

            if (item != null)
            {
                m_Ret = new Report.UserGroup
                {
                    ID = item.ID,
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description
                };
            }

            return m_Ret;
        }

        public async Task<IList<Models.UserGroup>> GetRoleFromUser(Guid userId, string branchIds)
        {
            var m_Ret = new List<Models.UserGroup>();

            var data = from f in appContext.UserGroup
                       where
                       (
                           branchIds.Contains(f.BranchId.ToString())
                       )
                       join a in
                           (
                               from c in appContext.UserRole
                               where c.IsActive == true && c.UserId == userId
                               select c
                            ) on f.ID equals a.GroupId into aug
                       from g in aug.DefaultIfEmpty()
                       select new
                       {
                           f.ID,
                           f.Name,
                           IsActive = g != null ? g.IsActive : default(System.Boolean?)
                       };


            //just provide only one type
            if (data != null)
            {

                foreach (var item in data)
                {

                    m_Ret.Add(new Models.UserGroup
                    {
                        ID = item.ID,
                        Name = item.Name,
                        IsActive = item.IsActive,
                        UserId = userId
                    });
                }
            }

            return m_Ret;
        }


        public long GetTabIndex()
        {
            var m_Ret = default(long);

            var item = appContext.UserGroup.Select(x => x.TabIndex).DefaultIfEmpty(0).Max();
            m_Ret = item + 1;

            return m_Ret;
        }
    }
}
