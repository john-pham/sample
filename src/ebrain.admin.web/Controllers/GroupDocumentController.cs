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
    public class GroupDocumentController : Controller
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        readonly IHostingEnvironment _env;

        public GroupDocumentController(IUnitOfWork unitOfWork, ILogger<GroupDocumentController> logger, IHostingEnvironment env)
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
            var bus = this._unitOfWork.GroupDocuments;
            var ret = from c in await bus.Search(filter, value, page, size, this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new GrpDocumentViewModel
                      {
                          ID = c.GroupDocumentId,
                          Code = c.GroupDocumentCode,
                          Name = c.GroupDocumentName,
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
        public async Task<GrpDocumentViewModel> Get(Guid index)
        {
            var c = await this._unitOfWork.GroupDocuments.FindById(index);

            var branch = new GrpDocumentViewModel
            {
                ID = c.GroupDocumentId,
                Code = c.GroupDocumentCode,
                Name = c.GroupDocumentName,
                Note = c.Note
            };
            
            return branch;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] GrpDocumentViewModel value)
        {
            if (ModelState.IsValid)
            {
                //
                var userId = new Guid(Utilities.GetUserId(this.User));
                //
                var grp = new GroupDocument
                {
                    GroupDocumentId = Guid.NewGuid(),
                    GroupDocumentCode = value.Code,
                    GroupDocumentName = value.Name,
                    Note = value.Note,
                  
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    
                };
                
                //commit
                var ret = await this._unitOfWork.GroupDocuments.Save(grp, value.ID);

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
                var ret = await this._unitOfWork.GroupDocuments.Delete(id);

                return Ok(ret);
            }

            return BadRequest(ModelState);
        }
        
    }
}
