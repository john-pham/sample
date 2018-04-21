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

        public async Task<bool> Update(IEnumerable<AccessRight> values)
        {

            foreach (var value in values)
            {
                var fea = await appContext.AccessRight.FirstOrDefaultAsync(x => x.FeatureID == value.FeatureID && x.GroupID == value.GroupID);

                if (fea == null)
                {
                    fea = new AccessRight
                    {
                        FeatureID = value.FeatureID,
                        GroupID = value.GroupID,
                        CreatedDate = DateTime.Now,
                    };
                    //
                    await appContext.AccessRight.AddAsync(fea);
                }

                fea.Value = value.Value;

                //
               
            }

            return await appContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid feature, Guid group)
        {
            var m_Ret = new bool();

            var item = await appContext.AccessRight.FirstOrDefaultAsync(x => x.FeatureID == feature && x.GroupID == group);

            if (item != null)
            {
                appContext.AccessRight.Remove(item);
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

            var items = from f in appContext.Feature
                        join a in
                            (
                                from c in appContext.AccessRight
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
                            Value = g != null ? g.Value : default(System.Int16?),
                            f.CreatedDate
                        };

            //FILTER
            if (featureGroupId != null)
            {
                items = items.Where(x => x.GroupID == featureGroupId);
            }

            var data = appContext.UserGroup.FirstOrDefault(x => x.ID == groupID);

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
                        FeatureId = item.ID,
                        FeatureName = item.Name,
                        GroupId = data.ID,
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
            var item = await (from f in appContext.Feature
                        join a in
                            (
                                from ar in appContext.AccessRight
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
                var data = await appContext.UserGroup.FirstOrDefaultAsync(x => x.ID == groupID);

                m_Ret = new Report.AccessRight
                {
                    FeatureId = item.ID,
                    FeatureName = item.Name,
                    GroupId = data.ID,
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

            m_Ret = (from c in appContext.AccessRight
                     where c.GroupID == groupID
                     select c.GroupID).Count();

            return m_Ret;
        }

    }
}
