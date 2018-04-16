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
using ebrain.admin.bc.Report;

namespace ebrain.admin.bc.Repositories
{
    public class MessengerRepository : Repository<Messenger>, IMessengerRepository
    {
        public int Total { get; private set; }
        public MessengerRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Messenger> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<MessengerReport> Search(Guid? userIdLogin, string filter, string value, string branchIds, int page, int size)
        {
            try
            {
                List<MessengerReport> data = new List<MessengerReport>();
                this.appContext.LoadStoredProc("dbo.sp_MessengerListBranchIds")
                               .WithSqlParam("@userId", userIdLogin)
                               .WithSqlParam("@branchIds", branchIds)
                               .WithSqlParam("@filterValue", value).ExecuteStoredProc((handler) =>
                               {
                                   data = handler.ReadToList<MessengerReport>().ToList();
                               });

                this.Total = data.Count();

                //
                if (size > 0 && page >= 0)
                {
                    data = (from c in data 
                            select c).Skip(page * size).Take(size).ToList();
                }

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Messenger> Save(Messenger value, MessengerBranch[] messageBranchs, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var itemExist = await Get(index, value.UpdatedBy);
            if (itemExist != null)
            {
                itemExist.MessengerCode = value.MessengerCode;
                itemExist.MessengerName = value.MessengerName;
                itemExist.Note = value.Note;
                itemExist.UpdatedBy = value.UpdatedBy;
                itemExist.UpdatedDate = DateTime.Now;
            }
            else
            {
                var result = await appContext.Messenger.AddAsync(value);
                foreach (var item in messageBranchs)
                {
                    item.BranchId = value.BranchId;
                    item.MessengerId = result.Entity.MessengerId;
                }
                await appContext.MessengerBranch.AddRangeAsync(messageBranchs);
                itemExist = result.Entity;
            }
            await appContext.SaveChangesAsync();
            return itemExist;
        }

        public async Task<bool> Delete(Guid id)
        {
            var itemExist = appContext.Messenger.FirstOrDefault(p => p.MessengerId == id);
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }

        public async Task<Messenger> Get(Guid? index, Guid userId)
        {
            var itemExist = await this.appContext.Messenger.FirstOrDefaultAsync(p => p.MessengerId == index);
            if (itemExist != null)
            {
                var itemRead = new MessengerRead
                {
                    MessengerReadId = Guid.NewGuid(),
                    MessengerId = itemExist.MessengerId,
                    UserId = userId,
                    Note = string.Empty,

                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };

                await appContext.MessengerRead.AddAsync(itemRead);
                await appContext.SaveChangesAsync();
            }
            return itemExist;
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
