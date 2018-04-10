using ebrain.admin.bc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ebrain.admin.bc.Utilities;
using ebrain.admin.bc.Report;
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
                       join a in
                           (
                               from c in appContext.UserRole
                               where c.IsActive == true
                               select c
                            ) on f.Id.ConvertStringToGuid() equals a.UserId into aug
                       from g in aug.DefaultIfEmpty()
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
                           IsActive = g != null ? g.IsActive : default(System.Boolean?),
                           BranchName = ubranch != null ? gb.BranchName : string.Empty,
                           GroupId = g != null ? g.GroupId : default(Guid?)
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
                            orderby c.Id
                            select c).Skip(page * size).Take(size);
                }

                foreach (var item in data)
                {
                    var grps = this.appContext.UserGroup.Where(p => p.ID == item.GroupId);
                    m_Ret.Add(new BranchUser
                    {
                        UserId = item.Id,
                        FullName = item.FullName,
                        UserName = item.UserName,
                        BranchName = item.BranchName,
                        IsActive = item.IsActive,
                        GroupName = grps != null && grps.Count() > 0 ? grps.Select(p => p.Name).Aggregate((i, j) => $"{i} - {j}") : string.Empty
                    });
                }
            }

            return m_Ret;
        }

    }
}
