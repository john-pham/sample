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
using ebrain.admin.bc.Utilities;

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
        public async Task<JsonResult> Search(string filter, string value, string grpId, int page, int size)
        {
            var bus = this._unitOfWork.Documents;
            var ret = await bus.Search(filter, value, grpId, page, size, this._unitOfWork.Branches.GetAllBranchOfUserString(userId));

            var list = new List<DocumentViewModel>();
            foreach (var c in ret)
            {
                var itemNew = new DocumentViewModel
                {
                    ID = c.DocumentId,
                    GrpId = c.GroupDocumentId,
                    Code = c.DocumentCode,
                    Name = c.DocumentName,
                    Path = c.Path.WebRootPathDocumentDownload(c.BranchId.ToString(), _env),
                    Note = c.Note,
                    GrDocumentName = c.GroupDocumentName
                };
                list.Add(itemNew);
            }
           
            return Json(new
            {
                Total = bus.Total,
                List = list
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
                    Path = value.Path,
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

                //save logo to physical file
                if (value.Logo != null && !string.IsNullOrEmpty(value.Logo.Name) && !string.IsNullOrEmpty(value.Logo.Value))
                {
                    //Convert Base64 Encoded string to Byte Array.
                    var base64String = value.Logo.Value;
                    var fileName = value.Logo.Name;
                    byte[] imageBytes = Convert.FromBase64String(base64String);

                    //Save the Byte Array as Image File.
                    string filePath = fileName.WebRootPathDocument(ret.BranchId.ToString(), _env);
                    imageBytes.WriteAllBytes(filePath);

                    //store filename to DB
                    grp.Path = fileName.GetFileName();

                    //save path
                    ret = await this._unitOfWork.Documents.Save(grp, ret.DocumentId);
                }


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
