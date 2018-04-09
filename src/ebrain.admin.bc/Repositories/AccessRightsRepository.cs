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
    public enum Behavior : byte
    {
        View = 1,
        Edit = 2,
        Delete = 4,
        Create = 8
    }

    public class AccessRightsRepository : Repository<AccessRight>, Interfaces.IAccessRightsRepository
    {
        public int Total { get; private set; }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public AccessRightsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<AccessRight> Update(AccessRight value, bool canView, bool canEdit, bool canDelete, bool canCreate)
        {
            if (value != null)
            {
                var fea = await appContext.AccessRights.FirstOrDefaultAsync(x => x.FeatureID == value.FeatureID && x.GroupID == value.GroupID);

                if (fea == null)
                {
                    fea = new AccessRight
                    {
                        FeatureID = value.FeatureID,
                        GroupID = value.GroupID,
                        CreatedDate = DateTime.Now,
                    };
                    //
                    await appContext.AccessRights.AddAsync(fea);
                }

                fea.Value = (byte)((canView ? (byte)Behavior.View : 0) +
                    (canEdit ? (byte)Behavior.Edit : 0) +
                    (canDelete ? (byte)Behavior.Delete : 0) +
                    (canCreate ? (byte)Behavior.Create : 0));
                fea.UpdatedDate = DateTime.Now;

                //
                if (await appContext.SaveChangesAsync() > 0)
                {
                    return fea;
                }
            }

            return value;

        }

        public async Task<bool> Delete(Guid feature, Guid group)
        {
            var m_Ret = new bool();

            var item = await appContext.AccessRights.FirstOrDefaultAsync(x => x.FeatureID == feature && x.GroupID == group);

            if (item != null)
            {
                appContext.AccessRights.Remove(item);
                //
                if (m_Ret = await appContext.SaveChangesAsync() > 0)
                {
                }
            }

            return m_Ret;
        }

        public async Task<IList<Report.AccessRight>> Search(Guid groupId, Guid? featureGroupId, int page, int size)
        {
            var m_Ret = new List<Report.AccessRight>();

            var groupID = groupId;

            var items = from f in appContext.Features
                        join a in
                            (
                                from c in appContext.AccessRights
                                where c.GroupID == groupID
                                select c
                                ) on f.ID equals a.FeatureID into aug
                        from g in aug.DefaultIfEmpty()
                        select new
                        {
                            f.ID,
                            f.Name,
                            f.Reference,
                            f.GroupID,
                            Parent = default(Guid?),
                            Value = g != null ? g.Value : 0,
                            f.CreatedDate
                        };

            //FILTER
            if (featureGroupId != null)
            {
                items = items.Where(x => x.GroupID == featureGroupId);
            }

            var data = appContext.UserGroups.FirstOrDefault(x => x.ID == groupID);

            //just provide only one type
            if (data != null)
            {
                //
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
                    var value = item.Value ?? 0;

                    m_Ret.Add(new Report.AccessRight
                    {
                        FeatureID = item.ID,
                        FeatureName = item.Name,
                        GroupID = data.ID,
                        GroupName = data.Name,
                        View = (((Behavior)value & Behavior.View) == Behavior.View),
                        Edit = (((Behavior)value & Behavior.Edit) == Behavior.Edit),
                        Delete = (((Behavior)value & Behavior.Delete) == Behavior.Delete),
                        Create = (((Behavior)value & Behavior.Create) == Behavior.Create),
                    });
                }
            }

            return m_Ret;
        }

        public async Task<Report.AccessRight> GetItem(Guid featureId, Guid groupId)
        {
            var m_Ret = default(Report.AccessRight);
            var groupID = groupId;
            var item = await (from f in appContext.Features
                        join a in
                            (
                                from ar in appContext.AccessRights
                                where ar.GroupID == groupID
                                select ar
                                ) on f.ID equals a.FeatureID into aug
                        from g in aug.DefaultIfEmpty()
                        where f.ID == featureId
                        select new
                        {
                            f.ID,
                            f.Name,
                            Value = g != null ? g.Value : 0,
                            f.CreatedDate
                        }).FirstOrDefaultAsync();

            if (item != null)
            {
                var data = await appContext.UserGroups.FirstOrDefaultAsync(x => x.ID == groupID);

                m_Ret = new Report.AccessRight
                {
                    FeatureID = item.ID,
                    FeatureName = item.Name,
                    GroupID = data.ID,
                    GroupName = data.Name,
                    View = (((Behavior)item.Value & Behavior.View) == Behavior.View),
                    Edit = (((Behavior)item.Value & Behavior.Edit) == Behavior.Edit),
                    Delete = (((Behavior)item.Value & Behavior.Delete) == Behavior.Delete),
                    Create = (((Behavior)item.Value & Behavior.Create) == Behavior.Create),
                };
            }

            return m_Ret;
        }

        public int Count(Guid group)
        {
            var m_Ret = 0;

            var groupID = group;

            m_Ret = (from c in appContext.AccessRights
                     where c.GroupID == groupID
                     select c.GroupID).Count();

            return m_Ret;
        }

    }
}
