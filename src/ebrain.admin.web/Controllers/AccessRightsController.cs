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
    public class AccessRightsController : Controller
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        readonly IHostingEnvironment _env;

        public AccessRightsController(IUnitOfWork unitOfWork, ILogger<AccessRightsController> logger, IHostingEnvironment env)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            this._env = env;
        }
        /*
        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> Search(string filter, string value, int page, int size)
        {
            var bus = this._unitOfWork.AccessRights;
            var ret = from c in await bus.Search(filter, value, page, size)
                      select new AccessRightViewModel
                      {
                          ID = c.BranchId,
                          Code = c.BranchCode,
                          Name = c.BranchName,
                          Email = c.Email,
                          Address = c.Address,
                          PhoneNumber = c.PhoneNumber,
                          Fax = c.FAX,
                          Logo = new FileViewModel
                          {
                              Name = string.Format("{0}.{1}", c.BranchId.ToString().Replace("-", string.Empty), c.LogoName)
                          }
                      };

            return Json(new
            {
                Total = bus.Total,
                List = ret
            });
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<AccessRightViewModel> Get(Guid featureId, Guid groupId)
        {
            var item = await this._unitOfWork.AccessRights.GetItem(featureId, groupId);

            var branch = new AccessRightViewModel
            {
                ID = item.BranchId,
                Code = item.BranchCode,
                Name = item.BranchName,
                Email = item.Email,
                Address = item.Address,
                PhoneNumber = item.PhoneNumber,
                Fax = item.FAX,
            };
            

            return branch;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] AccessRightViewModel value)
        {
            if (ModelState.IsValid)
            {
                //
                var userId = new Guid(Utilities.GetUserId(this.User));
                //
                var branch = new Branch
                {
                    BranchId = Guid.NewGuid(),
                    BranchCode = value.Code,
                    BranchName = value.Name,
                    Address = value.Address,
                    Email = value.Email,
                    PhoneNumber = value.PhoneNumber,
                    FAX = value.Fax,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    UserName = value.UserName,
                    Password = value.Password,
                    CPCode = value.CPCode,
                    RequestID = value.RequestID,
                    ServiceId = value.ServiceId,
                    CommandCode = value.CommandCode,
                    ContentType = value.ContentType
                };

                //save logo to physical file
                if (value.Logo != null &&
                    !string.IsNullOrEmpty(value.Logo.Name) &&
                    !string.IsNullOrEmpty(value.Logo.Value))
                {
                    //Convert Base64 Encoded string to Byte Array.
                    var base64String = value.Logo.Value;
                    var fileName = value.Logo.Name;
                    byte[] imageBytes = Convert.FromBase64String(base64String);

                    //Save the Byte Array as Image File.
                    string filePath = string.Format("{0}/uploads/logos/{1}.{2}",
                        this._env.WebRootPath,
                        branch.BranchId.ToString().Replace("-", string.Empty),
                        System.IO.Path.GetFileName(fileName));

                    System.IO.File.WriteAllBytes(filePath, imageBytes);
                    //store filename to DB
                    branch.LogoName = fileName;
                }

                //commit
                //var ret = await this._unitOfWork.AccessRights.Update(branch, value.ID);

                //return client side
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] Guid featureId, Guid groupId)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.AccessRights.Delete(featureId, groupId);

                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        private Guid userId
        {
            get
            {
                return new Guid(Utilities.GetUserId(this.User));
            }
        }
        */

    }
}
