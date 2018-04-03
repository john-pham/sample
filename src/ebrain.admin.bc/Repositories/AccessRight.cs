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

    public class AccessRightRepository : Repository<AccessRight>
    {
        public long Total { get; private set; }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public AccessRightRepository(ApplicationDbContext appContext) : base(context)
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
                await appContext.SaveChangesAsync();

                return fea;
            }

            return value;

        }

        public bool Delete(Guid feature, Guid group)
        {
            var m_Ret = new bool();

            {
                var item = appContext.Delete<AccessRight>(x => x.FeatureID == feature && x.GroupID == group);

                if (item != null)
                {
                    if (m_Ret.Value = appContext.Save() > 0)
                    {
                        m_Ret.ReturnID = feature;
                    }
                }
            }

            return m_Ret;
        }

        public IList<AccessRight> Search(Guid group, string feature, int page, int size)
        {
            var m_Ret = new List<AccessRight>();

            using (var appContext = new Entities(this.ConnectionString))
            {
                var groupID = group;

                var opts = from f in appContext.OutPriceTypes
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
                               Reference = default(Guid?),
                               Parent = _TAKEOUT,
                               Value = g != null ? g.Value : -1,
                               f.CreatedDate
                           };

                var ipts = from f in appContext.InPriceTypes
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
                               Reference = default(Guid?),
                               Parent = _TAKEIN,
                               Value = g != null ? g.Value : -1,
                               f.CreatedDate
                           };

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

                var temp = items.Where(f => f.Reference == _TAKEIN || f.Reference == _TAKEOUT);
                items = items.Where(f => f.Reference != _TAKEIN && f.Reference != _TAKEOUT);

                //FILTER
                if (feature != null)
                {
                    items = items.Where(x => x.Name.Contains(feature));
                    opts = opts.Where(x => x.Name.Contains(feature));
                    opts = opts.Where(x => x.Name.Contains(feature));
                }

                var data = appContext.UserGroups.FirstOrDefault(x => x.ID == groupID);

                //just provide only one type
                if (data != null)
                {
                    //
                    this.Total = items.Count();

                    //union take out & take in features
                    items = items.Concat(opts).Concat(ipts);
                    //
                    this.Total += ipts.Count() + opts.Count();

                    //
                    if (size > 0 && page >= 0)
                    {
                        items = (from c in items
                                 orderby c.CreatedDate
                                 select c).Skip(page * size).Take(size);
                    }

                    foreach (var item in items)
                    {
                        var value = item.Value;

                        if (value == -1 && (item.Parent == _TAKEIN || item.Parent == _TAKEOUT))
                        {
                            var def = temp.FirstOrDefault(x => x.Reference == (item.Parent == _TAKEIN ? _TAKEIN : _TAKEOUT));

                            value = def != null ? def.Value : 0;
                        }

                        m_Ret.Add(new AccessRight
                        {
                            FeatureID = item.ID,
                            Feature = item.Name,
                            GroupID = data.ID,
                            Group = data.Name,
                            View = (((Behavior)value & Behavior.View) == Behavior.View),
                            Edit = (((Behavior)value & Behavior.Edit) == Behavior.Edit),
                            Delete = (((Behavior)value & Behavior.Delete) == Behavior.Delete),
                            Create = (((Behavior)value & Behavior.Create) == Behavior.Create),
                        });
                    }
                }
            }

            return m_Ret;
        }

        public Session GetPermission(Guid groupID, Session value)
        {
            using (var appContext = new Entities(this.ConnectionString))
            {
                var items = from f in appContext.Features
                            join a in appContext.AccessRights on f.ID equals a.FeatureID
                            where a.GroupID == groupID
                            select new
                            {
                                f.ID,
                                Reference = f.Reference ?? Guid.Empty,
                                Parent = default(Guid?),
                                Value = a.Value,
                            };

                var opts = from f in appContext.OutPriceTypes
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
                               Reference = f.ID,
                               Parent = _TAKEOUT,
                               Value = g != null ? g.Value : -1
                           };

                var ipts = from f in appContext.InPriceTypes
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
                               Reference = f.ID,
                               Parent = _TAKEIN,
                               Value = g != null ? g.Value : -1,
                           };

                //
                items = items.Concat(ipts).Concat(opts);

                //just provide only one type
                value.Behaviors = new List<AccessRight>();
                value.Items = new Dictionary<Guid, byte>();
                foreach (var item in items)
                {
                    var data = item.Value;

                    if (data == -1 && (item.Parent == _TAKEIN || item.Parent == _TAKEOUT))
                    {
                        var def = items.FirstOrDefault(x => x.Reference == (item.Parent == _TAKEIN ? _TAKEIN : _TAKEOUT));

                        data = def?.Value;
                    }

                    if (data > 0)
                    {
                        value.Behaviors.Add(new AccessRight
                        {
                            FeatureID = item.ID,
                            View = ((Behavior)data & Behavior.View) == Behavior.View,
                            Edit = ((Behavior)data & Behavior.Edit) == Behavior.Edit,
                            Delete = ((Behavior)data & Behavior.Delete) == Behavior.Delete,
                            Create = ((Behavior)data & Behavior.Create) == Behavior.Create
                        });

                        //
                        value.Items.Add(item.Reference, (byte)data);
                    }
                }
            }

            return value;
        }

        public AccessRight GetItem(Guid feature, Guid group)
        {
            var m_Ret = default(AccessRight);
            var groupID = group;
            using (var appContext = new Entities(this.ConnectionString))
            {
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

                if (item == null)
                {
                    item = (from f in appContext.OutPriceTypes
                            join a in
                                 (
                                     from c in appContext.AccessRights
                                     where c.GroupID == groupID
                                     select c
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

                    if (item == null)
                    {
                        item = (from f in appContext.InPriceTypes
                                join a in
                                     (
                                         from c in appContext.AccessRights
                                         where c.GroupID == groupID
                                         select c
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
                    }
                }

                if (item != null)
                {
                    var data = appContext.UserGroups.FirstOrDefault(x => x.ID == groupID);

                    m_Ret = new AccessRight
                    {
                        FeatureID = item.ID,
                        Feature = item.Name,
                        GroupID = data.ID,
                        Group = data.Name,
                        View = (((Behavior)item.Value & Behavior.View) == Behavior.View),
                        Edit = (((Behavior)item.Value & Behavior.Edit) == Behavior.Edit),
                        Delete = (((Behavior)item.Value & Behavior.Delete) == Behavior.Delete),
                        Create = (((Behavior)item.Value & Behavior.Create) == Behavior.Create),
                    };
                }
            }

            return m_Ret;
        }

        public int Count(Guid group)
        {
            var m_Ret = 0;

            using (var appContext = new Entities(this.ConnectionString))
            {
                var groupID = group;

                m_Ret = (from c in appContext.AccessRights
                         where c.GroupID == groupID
                         select c.GroupID).Count();
            }

            return m_Ret;
        }

    }
}
