// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ebrain.admin.bc;
using Ebrain.ViewModels;
using ebrain.admin.bc.Models;
using Microsoft.Extensions.Logging;
using Ebrain.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace Ebrain.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        readonly IHostingEnvironment _env;

        public DocumentController(IUnitOfWork unitOfWork, ILogger<DocumentController> logger, IHostingEnvironment env)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            this._env = env;
        }

        private Guid userId
        {
            get
            {
                return new Guid(Utilities.GetUserId(this.User));
            }
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> Search(string filter, string value, int page, int size)
        {
            var bus = this._unitOfWork.Documents;
            var ret = from c in await bus.Search(filter, value, page, size, this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new DocumentViewModel
                      {
                          ID = c.DocumentId,
                          GrpId = c.GroupDocumentId,
                          Code = c.DocumentCode,
                          Name = c.DocumentName,
                          Path = c.Path,
                          Note = c.Note
                      };

            return Json(new
            {
                Total = bus.Total,
                List = ret
            });
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<DocumentViewModel> Get(Guid index)
        {
            var c = await this._unitOfWork.Documents.FindById(index);

            var branch = new DocumentViewModel
            {
                ID = c.DocumentId,
                GrpId = c.GroupDocumentId,
                Code = c.DocumentCode,
                Name = c.DocumentName,
                Path = c.Path,
                Note = c.Note
            };

            return branch;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] DocumentViewModel value)
        {
            if (ModelState.IsValid)
            {
                //
                var userId = new Guid(Utilities.GetUserId(this.User));
                //
                var grp = new Document
                {
                    DocumentId = Guid.NewGuid(),
                    DocumentCode = value.Code,
                    DocumentName = value.Name,
                    Note = value.Note,
                    GroupDocumentId = value.GrpId,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,

                };

                //commit
                var ret = await this._unitOfWork.Documents.Save(grp, value.ID);

                //return client side
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] Guid id)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.Documents.Delete(id);

                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

    }
}
