using ebrain.admin.bc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ebrain.admin.bc.Utilities;
/*
* Author:          John Pham
* CreatedDate Date:    07/23/2013
* 
*/
namespace ebrain.admin.bc.Repositories
{
    public class FeatureNoResultRepository : Repository<FeatureNoResult>, Interfaces.IFeatureNoResultRepository
    {
        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }

        public Task<bool> Execute(string command, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public FeatureNoResultRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void SenderEmailTemplate(Guid? featureId, Guid createdBy)
        {
            var branchId = createdBy.GetBranchOfCurrentUser(this.appContext);
            var featureNo = this.appContext.FeatureNotification.FirstOrDefault(p => p.FeatureId == featureId && p.BranchId == branchId);
            var feature = this.appContext.Feature.FirstOrDefault(p => p.ID == featureId);
            if (feature != null)
            {
               
            }
        }
        public void Save(FeatureNoResult value)
        {
            if (value != null)
            {
                appContext.FeatureNoResult.Add(value);
                appContext.SaveChangesAsync();
            }

        }
    }
}
