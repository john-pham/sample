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
    public class FeatureRepository : Repository<Feature>
    {
        public long Total { get; private set; }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public FeatureRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Feature> Update(Feature value)
        {
            var m_Ret = default(Feature);

            if (value != null)
            {
                var fea = await appContext.Features.FirstOrDefaultAsync(x => x.ID == value.ID);

                if (fea == null)
                {
                    var id = Guid.NewGuid();

                    if (value.Reference != null && value.Reference != Guid.Empty)
                    {
                        id = value.Reference ?? Guid.NewGuid();
                    }

                    fea = new Feature
                    {
                        ID = value.ID = id,
                        Reference = value.Reference,
                        CreatedDate = DateTime.Now,
                    };
                    //
                    appContext.Add(fea);
                }

                fea.Name = value.Name;
                fea.Url = value.Url;
                fea.GroupID = value.GroupID;
                fea.ReferenceItem = value.ReferenceItem;
                fea.Description = value.Description;
                fea.UpdatedDate = DateTime.Now;

                //
                if (await appContext.SaveChangesAsync() > 0)
                {
                    m_Ret = fea;
                }
            }

            return m_Ret;

        }

        public async Task<bool> Delete(Guid index)
        {
            var m_Ret = false;

            var item = await appContext.Features.FirstOrDefaultAsync(x => x.ID == index);

            if (item != null)
            {
                appContext.Features.Remove(item);
                //
                if (m_Ret = await appContext.SaveChangesAsync() > 0)
                {
                }
            }

            return m_Ret;
        }

        public IList<Report.Feature> Search(string name, string value, int page, int size)
        {
            var m_Ret = new List<Report.Feature>();

            var items = from f in appContext.Features
                        join fg in appContext.FeatureGroups on f.GroupID equals fg.ID into fgs
                        from g in fgs.DefaultIfEmpty()
                        select new
                        {
                            f.ID,
                            f.Reference,
                            f.Name,
                            f.Url,
                            f.GroupID,
                            Group = g != null ? g.Name : string.Empty,
                            f.Description,
                            f.CreatedDate
                        };

            //FILTER
            if (!string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrEmpty(name)) name = name.ToUpper();

                switch (name)
                {
                    case "URL":
                        items = items.Where(x => x.Url.Contains(value));

                        break;
                    case "NAME":
                        items = items.Where(x => x.Name.Contains(value));

                        break;
                    case "DESCRIPTION":
                        items = items.Where(x => x.Description.Contains(value));

                        break;
                    default:
                        items = items.Where(x => x.Url.Contains(value) ||
                            x.Name.Contains(value) ||
                            x.Description.Contains(value));

                        break;
                }
            }

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
                m_Ret.Add(new Report.Feature
                {
                    ID = item.ID,
                    Reference = item.Reference,
                    Name = item.Name,
                    Url = item.Url,
                    GroupID = item.GroupID,
                    GroupName = item.Group,
                    Description = item.Description
                });
            }

            return m_Ret;
        }

        public Report.Feature GetItem(Guid index)
        {
            var m_Ret = default(Report.Feature);

            var item = (from f in appContext.Features
                        join fg in appContext.FeatureGroups on f.GroupID equals fg.ID into fgs
                        from g in fgs.DefaultIfEmpty()
                        where f.ID == index
                        select new
                        {
                            f.ID,
                            f.Reference,
                            f.Name,
                            f.Url,
                            f.GroupID,
                            Group = g != null ? g.Name : string.Empty,
                            f.Description,
                            f.CreatedDate
                        }).FirstOrDefault();

            if (item != null)
            {
                m_Ret = new Report.Feature
                {
                    ID = item.ID,
                    Reference = item.Reference,
                    Name = item.Name,
                    Url = item.Url,
                    GroupID = item.GroupID,
                    GroupName = item.Group,
                    Description = item.Description
                };
            }

            return m_Ret;
        }

    }
}
