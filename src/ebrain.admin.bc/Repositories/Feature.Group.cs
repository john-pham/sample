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
    public class FeatureGroupRepository : Repository<FeatureGroup>, Interfaces.IFeatureGroupRepository
    {
        public int Total { get; private set; }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public FeatureGroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<FeatureGroup> Update(FeatureGroup value, Guid? index)
        {
            var m_Ret = default(FeatureGroup);

            if (value != null)
            {
                var fea = await appContext.FeatureGroup.FirstOrDefaultAsync(x => x.ID == index);

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
                        CreatedBy = value.CreatedBy
                    };
                    //
                    appContext.Add(fea);
                }

                fea.Name = value.Name;
                fea.Url = value.Url;
                fea.Description = value.Description;
                fea.UpdatedDate = DateTime.Now;
                fea.UpdatedBy = value.UpdatedBy;
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
            var m_Ret = new bool();

            var item = await appContext.FeatureGroup.FirstOrDefaultAsync(x => x.ID == index);

            if (item != null)
            {
                appContext.FeatureGroup.Remove(item);
                //
                if (m_Ret = await appContext.SaveChangesAsync() > 0)
                {
                }
            }

            return m_Ret;
        }

        public async Task<IList<Report.FeatureGroup>> Search(string value, int page, int size)
        {
            var m_Ret = new List<Report.FeatureGroup>();

            var items = from f in appContext.FeatureGroup
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
                items = items.Where(x => x.Url.Contains(value) ||
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
                m_Ret.Add(new Report.FeatureGroup
                {
                    ID = item.ID,
                    Name = item.Name,
                    Description = item.Description
                });
            }

            return m_Ret;
        }

        public async Task<Report.FeatureGroup> GetItem(Guid index)
        {
            var m_Ret = default(Report.FeatureGroup);

            var item = await (from f in appContext.FeatureGroup
                        where f.ID == index
                        select new
                        {
                            f.ID,
                            f.Reference,
                            f.Name,
                            f.Url,
                            f.Description,
                            f.CreatedDate
                        }).FirstOrDefaultAsync();

            if (item != null)
            {
                m_Ret = new Report.FeatureGroup
                {
                    ID = item.ID,
                    Name = item.Name,
                    Description = item.Description
                };
            }

            return m_Ret;
        }

    }
}
