// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ebrain.admin.bc.Models;
using ebrain.admin.bc.Repositories.Interfaces;
using ebrain.admin.bc.Utilities;
namespace ebrain.admin.bc.Repositories
{
    public class SMSRepository : Repository<SMS>, ISMSRepository
    {
        public int Total { get; private set; }

        public SMSRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SMS>> Search(string filter, string value, int page, int size, string branchIds)
        {
            var list = from c in this.appContext.SMS
                       where !c.IsDeleted && branchIds.Contains(c.BranchId.ToString())
                       select c;

            //
            this.Total = list.Count();

            if (size > 0 && page >= 0)
            {
                list = (from c in list
                        orderby c.CreatedDate descending
                        select c).Skip(page * size).Take(size);
            }

            foreach (var item in list)
            {
                var itemBranch = await this.appContext.Branch.FirstOrDefaultAsync(p => p.BranchId == item.BranchId);
                if (itemBranch != null)
                {
                    item.BranchName = itemBranch.BranchName;
                }
            }

            return await list.ToListAsync();
        }

        public async Task<SMS> Get(Guid? index)
        {
            return await this.appContext.SMS.FirstOrDefaultAsync(p => p.SMSId == index);
        }

        public async Task<SMS> Save(SMS value, Guid? oldId)
        {
            var str = SendSMS(value.CreatedBy, value.Phone, value.Body);

            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            value.Result = await str;

            var item = await appContext.SMS.FirstOrDefaultAsync(x => x.SMSId == oldId);

            if (item != null)
            {

            }
            else
            {
                var result = await appContext.SMS.AddAsync(value);
                item = result.Entity;
            }

            await appContext.SaveChangesAsync();
            //
            return item;
        }

        public async Task<string> SendSMS(Guid userId, string phone, string body)
        {
            var result = string.Empty;
            try
            {
                var branchId = userId.GetBranchOfCurrentUser(this.appContext);
                //get config
                var itemConfig = this.appContext.BranchSMS.FirstOrDefault(p => p.BranchId == branchId);
                if (itemConfig != null)
                {
                    smsviettel.CcApiClient sms = new smsviettel.CcApiClient();
                    var task = await sms.wsCpMtAsync(
                            itemConfig.UserName,
                            itemConfig.Password,
                            itemConfig.CPCode,
                            itemConfig.RequestID,
                            phone.FixedPhone(),
                            phone.FixedPhone(),
                            itemConfig.ServiceId,
                            itemConfig.CommandCode,
                            body,
                            itemConfig.ContentType);
                    result = task.@return.message;
                }
                else
                {
                    result = "Not yet config SMS";
                }
            }
            catch (Exception ex)
            {
                result = ex.InnerException.ToString();
            }
            return result;
        }

        public async Task<bool> Delete(Guid id)
        {
            return true;
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
