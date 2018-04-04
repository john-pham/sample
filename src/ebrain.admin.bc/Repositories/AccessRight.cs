﻿using ebrain.admin.bc.Models;
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

    public class AccessRightRepository : Repository<AccessRight>
    {
        public long Total { get; private set; }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public AccessRightRepository(ApplicationDbContext context) : base(context)
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

        public IList<Report.AccessRight> Search(Guid group, string feature, int page, int size)
        {
            var m_Ret = new List<Report.AccessRight>();

            var groupID = group;

            var items = from f in appContext.Features
                        join a in
                            (
                                from c in appContext.AccessRights
                                where c.GroupID == groupID
                                select c
                                ) on f.ID equals a.FeatureID into aug
                        from g in aug.DefaultIfEmpty()
                            //where f.Reference != _TAKEIN && f.Reference != _TAKEOUT
                        select new
                        {
                            f.ID,
                            f.Name,
                            f.Reference,
                            Parent = default(Guid?),
                            Value = g != null ? g.Value : 0,
                            f.CreatedDate
                        };

            //FILTER
            if (feature != null)
            {
                items = items.Where(x => x.Name.Contains(feature));
            }

            var data = appContext.UserGroups.FirstOrDefault(x => x.ID == groupID);

            //just provide only one type
            if (data != null)
            {
                //
                this.Total = items.Count();

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

        //public Session GetPermission(Guid groupID, Session value)
        //{
        //    using (var appContext = new Entities(this.ConnectionString))
        //    {
        //        var items = from f in appContext.Features
        //                    join a in appContext.AccessRights on f.ID equals a.FeatureID
        //                    where a.GroupID == groupID
        //                    select new
        //                    {
        //                        f.ID,
        //                        Reference = f.Reference ?? Guid.Empty,
        //                        Parent = default(Guid?),
        //                        Value = a.Value,
        //                    };

        //        //just provide only one type
        //        value.Behaviors = new List<AccessRight>();
        //        value.Items = new Dictionary<Guid, byte>();
        //        foreach (var item in items)
        //        {
        //            var data = item.Value;

        //            if (data == -1 && (item.Parent == _TAKEIN || item.Parent == _TAKEOUT))
        //            {
        //                var def = items.FirstOrDefault(x => x.Reference == (item.Parent == _TAKEIN ? _TAKEIN : _TAKEOUT));

        //                data = def?.Value;
        //            }

        //            if (data > 0)
        //            {
        //                value.Behaviors.Add(new AccessRight
        //                {
        //                    FeatureID = item.ID,
        //                    View = ((Behavior)data & Behavior.View) == Behavior.View,
        //                    Edit = ((Behavior)data & Behavior.Edit) == Behavior.Edit,
        //                    Delete = ((Behavior)data & Behavior.Delete) == Behavior.Delete,
        //                    Create = ((Behavior)data & Behavior.Create) == Behavior.Create
        //                });

        //                //
        //                value.Items.Add(item.Reference, (byte)data);
        //            }
        //        }
        //    }

        //    return value;
        //}

        public Report.AccessRight GetItem(Guid feature, Guid group)
        {
            var m_Ret = default(Report.AccessRight);
            var groupID = group;
            var item = (from f in appContext.Features
                        join a in
                            (
                                from ar in appContext.AccessRights
                                where ar.GroupID == groupID
                                select ar
                                ) on f.ID equals a.FeatureID into aug
                        from g in aug.DefaultIfEmpty()
                        where f.ID == feature
                        select new
                        {
                            f.ID,
                            f.Name,
                            Value = g != null ? g.Value : 0,
                            f.CreatedDate
                        }).FirstOrDefault();

            if (item != null)
            {
                var data = appContext.UserGroups.FirstOrDefault(x => x.ID == groupID);

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
