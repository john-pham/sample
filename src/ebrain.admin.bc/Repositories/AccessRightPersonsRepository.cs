using ebrain.admin.bc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ebrain.admin.bc.Repositories.Interfaces;
using ebrain.admin.bc.Utilities;

/*
 * Author:          John Pham
 * CreatedDate Date:    07/23/2013
 * 
 */
namespace ebrain.admin.bc.Repositories
{

    public class AccessRightPersonsRepository : Repository<AccessRightPerson>, IAccessRightPersonsRepository
    {
        public int Total { get; private set; }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public AccessRightPersonsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> Update(IEnumerable<AccessRightPerson> values)
        {

            foreach (var value in values)
            {
                var fea = await appContext.AccessRightPerson.FirstOrDefaultAsync
                    (x => x.FeatureId == value.FeatureId && x.UserId == value.UserId);

                if (fea == null)
                {
                    var result = await appContext.AccessRightPerson.AddAsync(value);
                    fea = result.Entity;
                }
                else
                {
                    fea.IsActive = value.IsActive;
                }

            }

            return await appContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid featureId, Guid userId)
        {
            var m_Ret = new bool();

            var item = await appContext.AccessRightPerson.FirstOrDefaultAsync
                (x => x.FeatureId == featureId && x.UserId == userId);

            if (item != null)
            {
                appContext.AccessRightPerson.Remove(item);
                //
                if (m_Ret = await appContext.SaveChangesAsync() > 0)
                {
                }
            }

            return m_Ret;
        }

        public async Task<IList<Report.AccessRightPerson>> Search(Guid? userId, Guid? featureGroupId, string filterValue, string branchIds)
        {
            try
            {
                List<Report.AccessRightPerson> someTypeList = new List<Report.AccessRightPerson>();
                this.appContext.LoadStoredProc("dbo.sp_AccessRightPerson")
                               .WithSqlParam("@featureId", featureGroupId)
                               .WithSqlParam("@userId", userId)
                               .WithSqlParam("@branchIds", branchIds)
                               .WithSqlParam("@filterValue", filterValue)
                               .ExecuteStoredProc((handler) =>
                               {
                                   someTypeList = handler.ReadToList<Report.AccessRightPerson>().ToList();
                               });

                return someTypeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Guid?> GetUserIdFromAccessRightPerson(Guid? featureId, Guid? userId)
        {
            var fea = await appContext.AccessRightPerson.FirstOrDefaultAsync
                        (
                            x => x.FeatureId == featureId
                            && x.UserId == userId && x.IsActive == true
                            && x.IsDeleted == false
                        );

            return fea != null ? null : userId;

        }



    }
}
