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
    [Security("ADC69968-D9E7-4C9E-AD15-206ED47A9D31")]
    public class FeatureGroupsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        readonly IHostingEnvironment _env;

        public FeatureGroupsController(IUnitOfWork unitOfWork, ILogger<DocumentController> logger, IHostingEnvironment env) : base(unitOfWork, logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            this._env = env;
        }

        [HttpGet("getall")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetAll()
        {
            var ugs = await this._unitOfWork.FeatureGroups.Search("", 0, 0);
            return Ok(ugs);
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> Search(string filter, string value, int page, int size)
        {
            var bus = this._unitOfWork.FeatureGroups;
            var ret = from c in await bus.Search(value, page, size)
                      select new FeatureGroupViewModel
                      {
                          ID = c.ID,
                          Description = c.Description,
                          Name = c.Name
                      };

            return Json(new
            {
                Total = bus.Total,
                List = ret
            });
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<FeatureGroupViewModel> Get(Guid index)
        {
            var c = await this._unitOfWork.FeatureGroups.GetItem(index);

            var grp = new FeatureGroupViewModel
            {
                ID = c.ID,
                Name = c.Name,
                Description = c.Description
            };

            return grp;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] FeatureGroupViewModel value, Guid? index)
        {
            if (ModelState.IsValid)
            {

                var grp = new FeatureGroup
                {
                    ID = Guid.NewGuid(),
                    Name = value.Name,
                    Description = value.Description,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };

                //commit
                var ret = await this._unitOfWork.FeatureGroups.Update(grp, value.ID);

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
                var ret = await this._unitOfWork.FeatureGroups.Delete(id);

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
