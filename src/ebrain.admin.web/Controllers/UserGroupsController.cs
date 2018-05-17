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
    [Security("9C100588-C478-47C8-BE15-40523BB6BA1B")]
    public class UserGroupsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        readonly IHostingEnvironment _env;

        public UserGroupsController(IUnitOfWork unitOfWork, ILogger<DocumentController> logger, IHostingEnvironment env) : base(unitOfWork, logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            this._env = env;
        }

        [HttpGet("getall")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetAll()
        {
            var ugs = await this._unitOfWork.UserGroups.Search("", this._unitOfWork.Branches.GetAllBranchOfUserString(userId), 0, 0);
            return Ok(ugs);
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> Search(string filter, string value, int page, int size)
        {
            var bus = this._unitOfWork.UserGroups;
            var ret = from c in await bus.Search(value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId), page, size)
                      select new UserGroupViewModel
                      {
                          ID = c.ID,
                          Code = c.Code,
                          Name = c.Name,
                          Note = c.Description
                      };

            return Json(new
            {
                Total = bus.Total,
                List = ret
            });
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<UserGroupViewModel> Get(Guid index)
        {
            var c = await this._unitOfWork.UserGroups.GetItem(index);

            var grp = new UserGroupViewModel
            {
                ID = c.ID,
                Code = c.Code,
                Name = c.Name
            };
            
            return grp;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UserGroupViewModel value, Guid? index)
        {
            if (ModelState.IsValid)
            {
               
                var grp = new UserGroup
                {
                    ID = Guid.NewGuid(),
                    Code = Guid.NewGuid().ToString(),
                    Name = value.Name,
                    Description = value.Note,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    UserId = userId,
                    IsActive = true
                    
                };

                //commit
                var ret = await this._unitOfWork.UserGroups.Update(grp, value.ID);

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
                var ret = await this._unitOfWork.UserGroups.Delete(id);

                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        private Guid userId
        {
            get
            {
                return Utilities.GetUserId(this.User);
            }
        }
    }
}
