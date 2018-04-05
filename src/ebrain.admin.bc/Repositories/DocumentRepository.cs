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
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public int Total { get; private set; }

        public DocumentRepository(ApplicationDbContext context) : base(context)
        { }

        public IEnumerable<Document> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Document>> Documents(string branchIds)
        {
            return await this.appContext.Document.Where(p => branchIds.Contains(p.BranchId.ToString())).ToListAsync();
        }
        public async Task<Document> FindById(Guid? id)
        {
            return await this.appContext.Document.FirstOrDefaultAsync(p => p.DocumentId == id);
        }

        public async Task<IEnumerable<Document>> Search(string filter, string value, string grpId, int page, int size, string branchIds)
        {
            var grps = this.appContext.Document.Where
                (
                    p => p.IsDeleted == false &&
                    branchIds.Contains(p.BranchId.ToString()) &&
                    (string.IsNullOrEmpty(grpId) || p.GroupDocumentId.ToString() == grpId) &&
                    (string.IsNullOrEmpty(value) || p.DocumentCode.Contains(value) || p.DocumentName.Contains(value))
                );

            this.Total = grps.Count();

            if (size > 0 && page >= 0)
            {
                grps = (from c in grps
                        orderby c.CreatedDate descending
                        select c).Skip(page * size).Take(size);
            }

            foreach (var item in grps)
            {
                var itemGrp = this.appContext.GroupDocument.FirstOrDefault(p => p.GroupDocumentId == item.GroupDocumentId);
                if (itemGrp != null)
                {
                    item.GroupDocumentName = itemGrp.GroupDocumentName;
                }
            }
            return grps;
        }

        public async Task<Document> Save(Document value, Guid? index)
        {
            value.BranchId = value.CreatedBy.GetBranchOfCurrentUser(this.appContext);
            var itemExist = await FindById(index);
            if (itemExist != null)
            {
                itemExist.GroupDocumentId = value.GroupDocumentId;
                itemExist.DocumentCode = value.DocumentCode;
                itemExist.DocumentName = value.DocumentName;
                itemExist.Note = value.Note;
                itemExist.UpdatedBy = value.UpdatedBy;
                itemExist.UpdatedDate = DateTime.Now;
                itemExist.Path = value.Path;
            }
            else
            {
                var result = await appContext.Document.AddAsync(value);
                itemExist = result.Entity;
            }
            await appContext.SaveChangesAsync();
            //
            return itemExist;
        }

        public async Task<bool> Delete(Guid? id)
        {
            var itemExist = appContext.Document.FirstOrDefault(p => p.DocumentId == id);
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
