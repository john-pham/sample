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
using ebrain.admin.bc.Repositories;

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

        [HttpGet("getall")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetAll()
        {
            var fea = await this._unitOfWork.FeatureGroups.Search("", 0, 0);
            var ugs = await this._unitOfWork.UserGroups.Search("", this._unitOfWork.Branches.GetAllBranchOfUserString(userId), 0, 0);

            return Ok(new
            {
                Features = fea.Select(c => new
                {
                    c.ID,
                    c.Name
                }),
                UserGroups = ugs.Select(c => new
                {
                    c.ID,
                    c.Name
                })
            });
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> Search(Guid groupId, Guid? featureGroupId, int page, int size)
        {
            var bus = this._unitOfWork.AccessRights;
            var ret = from c in await bus.Search(groupId, featureGroupId, page, size)
                      select new AccessRightViewModel
                      {
                          FeatureID = c.FeatureID,
                          FeatureName = c.FeatureName,
                          GroupID = c.GroupID,
                          GroupName = c.GroupName,
                          View = c.View,
                          Edit = c.Edit,
                          Create = c.Create,
                          Delete = c.Delete
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
                FeatureID = item.FeatureID,
                FeatureName = item.FeatureName,
                GroupID = item.GroupID,
                GroupName = item.GroupName,
                View = item.View,
                Edit = item.Edit,
                Create = item.Create,
                Delete = item.Delete
            };


            return branch;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] AccessRightViewModel[] values)
        {
            if (ModelState.IsValid)
            {
                //
                var userId = Utilities.GetUserId(this.User);

                var ar = values.Select(p => new AccessRight
                {
                    FeatureID = p.FeatureID,
                    GroupID = p.GroupID,
                    Value = (byte)((p.View ? (byte)Behavior.View : 0) +
                    (p.Edit ? (byte)Behavior.Edit : 0) +
                    (p.Delete ? (byte)Behavior.Delete : 0) +
                    (p.Create ? (byte)Behavior.Create : 0))
                });

                //commit
                var ret = await this._unitOfWork.AccessRights.Update(ar);

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
                return Utilities.GetUserId(this.User);
            }
        }

    }
}
