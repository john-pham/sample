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
    public class GroupDocumentRepository : Repository<GroupDocument>, IGroupDocumentRepository
    {
        public int Total { get; private set; }

        public GroupDocumentRepository(ApplicationDbContext context) : base(context)
        { }

        public IEnumerable<GroupDocument> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GroupDocument>> GroupDocuments(string branchIds)
        {
            return await this.appContext.GroupDocument.Where(p => branchIds.Contains(p.BranchId.ToString())).ToListAsync();
        }
        public async Task<GroupDocument> FindById(Guid? id)
        {
            return await this.appContext.GroupDocument.FirstOrDefaultAsync(p => p.GroupDocumentId == id);
        }

        public async Task<IEnumerable<GroupDocument>> GetAll(string branchIds)
        {
            return this.appContext.GroupDocument.Where
                (
                    p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                );
        }
        public async Task<IEnumerable<GroupDocument>> Search(string filter, string value, int page, int size, string branchIds)
        {
            var grps = this.appContext.GroupDocument.Where
                (
                    p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString())
                );

            this.Total = grps.Count();

            if (size > 0 && page >= 0)
            {
                grps = (from c in grps
                        orderby c.CreatedDate descending
                        select c).Skip(page * size).Take(size);
            }

            return grps;
        }

        public async Task<GroupDocument> Save(GroupDocument value, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var itemExist = await FindById(index);
            if (itemExist != null)
            {
                itemExist.GroupDocumentCode = value.GroupDocumentCode;
                itemExist.GroupDocumentName = value.GroupDocumentName;
                itemExist.Note = value.Note;
                itemExist.UpdatedBy = value.UpdatedBy;
                itemExist.UpdatedDate = DateTime.Now;
            }
            else
            {
                var result = await appContext.GroupDocument.AddAsync(value);
                itemExist = result.Entity;
            }
            await appContext.SaveChangesAsync();
            //
            return itemExist;
        }

        public async Task<bool> Delete(Guid? id)
        {
            //Check exist in materials
            var grpExist = this.appContext.Document.FirstOrDefault(p => p.GroupDocumentId == id);
            if (grpExist != null) throw new Exception("Exist in documents");

            var itemExist = appContext.GroupDocument.FirstOrDefault(p => p.GroupDocumentId == id);
            if (itemExist != null)
            {
                itemExist.IsDeleted = true;
            }
            await appContext.SaveChangesAsync();
            return true;
        }
        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
