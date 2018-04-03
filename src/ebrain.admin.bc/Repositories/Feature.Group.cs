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
    public class FeatureGroupRepository : Repository<FeatureGroup>
    {
        public long Total { get; private set; }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public FeatureGroupRepository(ApplicationDbContext appContext) : base(context)
        {
        }

        public bool Update(FeatureGroup value)
        {
            var m_Ret = new bool();

            if (value != null)
            {
                using (var appContext = new Entities(this.ConnectionString))
                {
                    var fea = appContext.Edit< FeatureGroup>(x => x.ID == value.ID);

                    if (fea == null)
                    {
                        var id = Guid.NewGuid();

                        if (value.Reference != null && value.Reference != Guid.Empty)
                        {
                            id = value.Reference ?? Guid.NewGuid();
                        }

                        fea = new FeatureGroup
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
                    fea.Description = value.Description;
                    fea.UpdatedDate = DateTime.Now;

                    //
                    if(m_Ret.Value = appContext.Save() > 0)
                    {
                        m_Ret.ReturnID = fea.ID;
                    }
                }
            }

            return m_Ret;

        }

        public bool Delete(Guid index)
        {
            var m_Ret = new bool();

            using (var appContext = new Entities(this.ConnectionString))
            {
                var item = appContext.Delete< FeatureGroup>(x => x.ID == index);

                if (item != null)
                {
                    if(m_Ret.Value = appContext.Save() > 0)
                    {
                        m_Ret.ReturnID = index;
                    }
                }
            }

            return m_Ret;
        }

        public IList<FeatureGroup> Search(string name, string value, int page, int size)
        {
            var m_Ret = new List<FeatureGroup>();

            using (var appContext = new Entities(this.ConnectionString))
            {
                var items = from f in appContext.FeatureGroups
                            select new
                            {
                                f.ID,
                                f.Reference,
                                f.Name,
                                f.Url,
                                f.Description,
                                f.CreatedDate
                            };

                //FILTER
                if (!string.IsNullOrEmpty(value))
                {
                    if(!string.IsNullOrEmpty(name))name = name.ToUpper();

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
                    m_Ret.Add(new FeatureGroup
                    {
                        ID = item.ID,
                        Reference = item.Reference,
                        Name = item.Name,
                        Url = item.Url,
                        Description = item.Description
                    });
                }
            }

            return m_Ret;
        }

        public FeatureGroup GetItem(Guid index)
        {
            var m_Ret = default(FeatureGroup);

            using (var appContext = new Entities(this.ConnectionString))
            {
                var item = (from f in appContext.FeatureGroups
                           where f.ID == index
                           select new
                           {
                               f.ID,
                               f.Reference,
                               f.Name,
                               f.Url,
                               f.Description,
                               f.CreatedDate
                           }).FirstOrDefault();

                if (item != null)
                {
                    m_Ret = new FeatureGroup
                    {
                        ID = item.ID,
                        Reference = item.Reference,
                        Name = item.Name,
                        Url = item.Url,
                        Description = item.Description
                    };
                }
            }

            return m_Ret;
        }

    }
}
