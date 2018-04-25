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
    [Security("ADC69968-D9E7-4C9E-AD15-206ED47A6F18", "ADC69968-D9E7-4C9E-AD15-206ED47A6F19")]
    public class SMSController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        readonly IHostingEnvironment _env;

        public SMSController(IUnitOfWork unitOfWork, ILogger<SMSController> logger, IHostingEnvironment env) : base(unitOfWork, logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            this._env = env;
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> Search(string filter, string value, int page, int size)
        {
            var bus = this._unitOfWork.SMSs;
            var ret = from c in await bus.Search(filter, value, page, size, 
                this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new SMSViewModel
                      {
                          ID = c.SMSId,
                          BranchId = c.BranchId,
                          Phone = c.Phone,
                          Body = c.Body,
                          BranchName = c.BranchName,
                          Result = c.Result
                      };

            return Json(new
            {
                Total = bus.Total,
                List = ret
            });
        }
        

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SMSViewModel value)
        {
            if (ModelState.IsValid)
            {
                //
                var userId = Utilities.GetUserId(this.User);
                //
                var SMS = new SMS
                {
                    SMSId = Guid.NewGuid(),
                    Body = value.Body,
                    Phone = value.Phone,
                 
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                   
                };
                
                //commit
                var ret = await this._unitOfWork.SMSs.Save(SMS, value.ID);

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
