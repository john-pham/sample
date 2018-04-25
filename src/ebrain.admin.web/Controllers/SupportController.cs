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
    [Security("ADC69968-D9E7-4C9E-AD15-206ED47A6D30")]
    public class SupportController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        readonly IHostingEnvironment _env;

        public SupportController(IUnitOfWork unitOfWork, ILogger<SupportController> logger, IHostingEnvironment env) : base(unitOfWork, logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            this._env = env;
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> Search(string filter, string value, int page, int size)
        {
            var bus = this._unitOfWork.Supports;
            var ret = from c in bus.Search(value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId), page, size)
                      select new SupportViewModel
                      {
                          SupportId = c.SupportId,
                          SupportCode = c.SupportCode,
                          SupportName = c.SupportName,
                          Title = c.Title,
                          Email = c.Email,
                          Phone = c.Phone,
                          BranchId = c.BranchId,
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
        public async Task<SupportViewModel> Get(Guid index)
        {
            var c = await this._unitOfWork.Supports.Get(index, userId);

            var datas = new SupportViewModel
            {
                SupportId = c.SupportId,
                SupportCode = c.SupportCode,
                SupportName = c.SupportName,
                Title = c.Title,
                Phone = c.Phone,
                Email = c.Email,
                CreateDate = c.CreatedDate,
                BranchId = c.BranchId,
                Note = c.Note
            };

            return datas;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SupportViewModel value)
        {
            if (ModelState.IsValid)
            {
                //
                var userId = Utilities.GetUserId(this.User);
                //
                var m = new Support
                {
                    SupportId = Guid.NewGuid(),
                    SupportCode = Guid.NewGuid().ToString(),
                    SupportName = value.SupportName,
                    Title = value.Title,
                    Phone = value.Phone,
                    Email = value.Email,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Note = value.Note
                };
                
                var ret = await this._unitOfWork.Supports.Save(m, value.SupportId);

                //return client side
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
