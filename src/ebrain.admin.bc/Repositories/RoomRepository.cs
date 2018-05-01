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
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public int Total { get; private set; }
        public RoomRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Room> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<Room>> Search(string filter, string value, string branchIds, int page, int size)
        {
            var someTypeList = this.appContext.Room.Where(p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                );
            //paging
            this.Total = someTypeList.Count();
            if (size > 0 && page >= 0)
            {
                someTypeList = (from c in someTypeList select c).Skip(page * size).Take(size);
            }
            return someTypeList;
        }

        public async Task<Room> Save(Room value, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var itemExist = await Get(index);
            if (itemExist != null)
            {
                itemExist.RoomCode = value.RoomCode;
                itemExist.RoomName = value.RoomName;
                itemExist.Note = value.Note;
                itemExist.UpdatedBy = value.UpdatedBy;
                itemExist.UpdatedDate = DateTime.Now;
            }
            else
            {
                var result = await appContext.Room.AddAsync(value);
                itemExist = result.Entity;
            }
            await appContext.SaveChangesAsync();
            return itemExist;
        }

        public async Task<bool> Delete(string id)
        {
            var itemExist = appContext.Room.FirstOrDefault(p => p.RoomId.Equals(new Guid(id)));
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }

        public Task<Room> Get(Guid? index)
        {
            return this.appContext.Room.FirstOrDefaultAsync(p => p.RoomId == index);
        }

        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
