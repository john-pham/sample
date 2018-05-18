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
    [Security("ADC69968-D9E7-4C9E-AD15-206ED47A9D32")]
    public class UserRolesController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        readonly IHostingEnvironment _env;

        public UserRolesController(IUnitOfWork unitOfWork, ILogger<UserRolesController> logger, IHostingEnvironment env) : base(unitOfWork, logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            this._env = env;
        }

        [HttpGet("getall")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetAll()
        {
            var ugs = await this._unitOfWork.UserRoles.Search("", this._unitOfWork.Branches.GetAllBranchOfUserString(userId), 0, 0);
            return Ok(ugs);
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> Search(string filter, string value, int page, int size)
        {
            var bus = this._unitOfWork.UserRoles;
            var ret = from c in await bus.Search(value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId), page, size)
                      select new UserRoleViewModel
                      {
                          UserId = c.UserId.ConvertStringToGuid(),
                          UserName = c.UserName,
                          FullName = c.FullName,
                          GroupName = c.GroupName,
                          BranchName = c.BranchName
                      };

            return Json(new
            {
                Total = bus.Total,
                List = ret
            });
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<UserRoleViewModel>> Get(Guid userId)
        {
            var results = await this._unitOfWork.UserGroups.GetRoleFromUser(userId, this._unitOfWork.Branches.GetAllBranchOfUserString(this.userId));

            var datas = results.Select(c => new UserRoleViewModel
            {
                GroupId = c.ID.ToString(),
                Code = c.Code,
                Name = c.Name,
                IsActive = c.IsActive,
                UserId = c.UserId
            });

            return datas;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UserRoleViewModel[] values)
        {
            if (ModelState.IsValid)
            {

                var grps = values.Select(c => new UserRole
                {
                    GroupId = c.GroupId.ConvertStringToGuid(),
                    UserId = c.UserId.HasValue ? c.UserId.Value : Guid.Empty,
                    IsActive = c.IsActive.HasValue ? c.IsActive.Value : false,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,

                });

                //commit
                var ret = await this._unitOfWork.UserRoles.Update(grps);

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
                var ret = await this._unitOfWork.UserRoles.Delete(id);

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
