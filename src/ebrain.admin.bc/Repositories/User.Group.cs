using ebrain.admin.bc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public async Task<UserGroup> Update(UserGroup value)
        {
            var m_Ret = default(UserGroup);

            if (value != null)
            {
                var cus = await appContext.UserGroups.FirstOrDefaultAsync(x => x.ID == value.ID);

                if (cus == null)
                {
                    cus = new UserGroup
                    {
                        ID = value.ID = Guid.NewGuid(),
                        Code = Guid.NewGuid().ToString(),
                        CreatedDate = DateTime.Now,
                    };
                    //
                    appContext.Add(cus);
                }

                //user.Code = value.Code;
                cus.Name = value.Name;
                cus.Description = value.Description;
                cus.UpdatedDate = DateTime.Now;

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

            var item = await appContext.UserGroups.FirstOrDefaultAsync(x => x.ID == index);

            if (item != null && appContext.Users.Count(x => x.GroupId == index) <= 0)
            {
                appContext.AccessRights.RemoveRange(appContext.AccessRights.Where(x => x.GroupID == index));
                appContext.UserGroups.Remove(item);
                //
                if (m_Ret = await appContext.SaveChangesAsync() > 0)
                {
                }
            }

            return m_Ret;
        }

        public async Task<IList<Report.UserGroup>> Search(string value, int page, int size)
        {
            var m_Ret = new List<Report.UserGroup>();

            var items = from c in appContext.UserGroups
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
                         orderby c.CreatedDate
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

            var item = await appContext.UserGroups.FirstOrDefaultAsync(x => (x.ID == index));

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

        public long GetTabIndex()
        {
            var m_Ret = default(long);

            var item = appContext.UserGroups.Select(x => x.TabIndex).DefaultIfEmpty(0).Max();
            m_Ret = item + 1;

            return m_Ret;
        }
    }
}
