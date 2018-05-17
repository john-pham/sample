using ebrain.admin.bc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ebrain.admin.bc.Utilities;
using ebrain.admin.bc.Report;
using System.Text;
/*
* Author:          John Pham
* CreatedDate Date:    07/23/2013
* 
*/
namespace ebrain.admin.bc.Repositories
{

    public class UserRoleRepository : Repository<UserRole>, Interfaces.IUserRoleRepository
    {
        public int Total { get; private set; }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> Update(IEnumerable<UserRole> values)
        {

            foreach (var value in values)
            {
                var fea = await appContext.UserRole.FirstOrDefaultAsync(x => x.UserId == value.UserId && x.GroupId == value.GroupId);

                if (fea == null)
                {
                    fea = new UserRole
                    {
                        UserId = value.UserId,
                        GroupId = value.GroupId,
                        CreatedDate = DateTime.Now,
                        CreatedBy = value.CreatedBy
                    };
                    //
                    await appContext.UserRole.AddAsync(fea);
                }
                fea.UpdatedBy = value.UpdatedBy;
                fea.UpdatedDate = DateTime.Now;
                fea.IsActive = value.IsActive;
            }

            return await appContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid userId)
        {
            var m_Ret = new bool();

            var items = appContext.UserRole.Where(x => x.UserId == userId);

            if (items != null && items.Count() > 0)
            {
                appContext.UserRole.RemoveRange(items);
                //
                if (m_Ret = await appContext.SaveChangesAsync() > 0)
                {
                }
            }

            return m_Ret;
        }

        public async Task<IList<BranchUser>> Search(string value, string branchIds, int page, int size)
        {
            var m_Ret = new List<BranchUser>();

            var data = from f in appContext.Users
                       where
                       (
                           (string.IsNullOrEmpty(value) || f.FullName.Contains(value))
                           && branchIds.Contains(f.BranchId.ToString())
                       )
                       join b in
                            (
                                from br in appContext.Branch
                                where br.IsDeleted == false
                                select br
                            ) on f.BranchId equals b.BranchId into ubranch
                       from gb in ubranch.DefaultIfEmpty()
                       select new
                       {
                           f.Id,
                           f.FullName,
                           f.UserName,
                           f.BranchId,
                           BranchName = ubranch != null ? gb.BranchName : string.Empty,
                           f.CreatedDate
                       };


            //just provide only one type
            if (data != null)
            {
                //
                this.Total = await data.CountAsync();

                //
                if (size > 0 && page >= 0)
                {
                    data = (from c in data
                            orderby c.CreatedDate descending
                            select c).Skip(page * size).Take(size);
                }

                foreach (var item in data)
                {
                    var userRoles = this.appContext.UserRole.Where(p => p.UserId == item.Id.ConvertStringToGuid() && p.IsActive == true);
                    var groupName = new StringBuilder();
                    foreach (var role in userRoles)
                    {
                        var grp = this.appContext.UserGroup.FirstOrDefault(p => p.ID == role.GroupId);
                        if (grp != null) groupName.Append(groupName.Length > 0 ? $" - {grp.Name}" : grp.Name);
                    }

                    m_Ret.Add(new BranchUser
                    {
                        UserId = item.Id,
                        FullName = item.FullName,
                        UserName = item.UserName,
                        BranchName = item.BranchName,
                        GroupName = groupName.ToString()
                    });
                }
            }

            return m_Ret;
        }

        public async Task<IList<Report.AccessRight>> GetAll(Guid userId)
        {
            var data = await (
                from a in appContext.AccessRight
                join r in appContext.UserRole on a.GroupID equals r.GroupId
                where r.UserId == userId && r.IsActive
                select new Report.AccessRight
                {
                    FeatureId = a.FeatureID,
                    //FeatureName = a.fe
                    GroupId = a.GroupID,
                    //GroupName = a.
                    View = (((Behavior)(a.Value ?? 0) & Behavior.View) == Behavior.View),
                    Edit = (((Behavior)(a.Value ?? 0) & Behavior.Edit) == Behavior.Edit),
                    Delete = (((Behavior)(a.Value ?? 0) & Behavior.Delete) == Behavior.Delete),
                    Create = (((Behavior)(a.Value ?? 0) & Behavior.Create) == Behavior.Create),
                }).ToListAsync();

            return data;
        }
    }
}
