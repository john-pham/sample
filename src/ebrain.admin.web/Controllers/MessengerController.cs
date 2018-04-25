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
    public class MessengerController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        readonly IHostingEnvironment _env;

        public MessengerController(IUnitOfWork unitOfWork, ILogger<MessengerController> logger, IHostingEnvironment env) : base(unitOfWork, logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            this._env = env;
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> Search(string filter, string value, int page, int size)
        {
            var bus = this._unitOfWork.Messengers;
            var ret = from c in bus.Search(userId, filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId), page, size)
                      select new MessengerViewModel
                      {
                          MessengerId = c.MessengerId,
                          MessengerCode = c.MessengerCode,
                          MessengerName = c.MessengerName,
                          MessengerTitle = c.MessengerTitle,
                          IsRead = c.IsRead,
                          BranchId = c.BranchId,
                          BranchName = c.BranchName,
                          CreateDate = c.CreateDate
                      };

            return Json(new
            {
                Total = bus.Total,
                List = ret
            });
        }

        [HttpGet("getnewmessenger")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> GetNewMessenger()
        {
            var bus = this._unitOfWork.Messengers;
            var ret = from c in bus.Search(userId, "", "", this._unitOfWork.Branches.GetAllBranchOfUserString(userId), 0, 0)
                      where c.IsRead == false
                      select new MessengerViewModel
                      {
                          MessengerId = c.MessengerId,
                          MessengerCode = c.MessengerCode,
                          MessengerName = c.MessengerName,
                          MessengerTitle = c.MessengerTitle,
                          IsRead = c.IsRead,
                          BranchId = c.BranchId,
                          BranchName = c.BranchName,
                          CreateDate = c.CreateDate
                      };

            return Json(new
            {
                Total = ret.Count(),
                List = ret.Count() > 5 ? ret.Take(5) : ret
            });
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<MessengerViewModel> Get(Guid index)
        {
            var c = await this._unitOfWork.Messengers.Get(index, userId);

            var datas = new MessengerViewModel
            {
                MessengerId = c.MessengerId,
                MessengerCode = c.MessengerCode,
                MessengerName = c.MessengerName,
                MessengerTitle = c.MessengerTitle,
                BranchId = c.BranchId,
            };

            return datas;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] MessengerViewModel value)
        {
            if (ModelState.IsValid)
            {
                //
                var userId = Utilities.GetUserId(this.User);
                //
                var m = new Messenger
                {
                    MessengerId = Guid.NewGuid(),
                    MessengerCode = value.MessengerCode,
                    MessengerName = value.MessengerName,
                    MessengerTitle = value.MessengerTitle,

                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,

                };

                var branchIds = this._unitOfWork.Branches.GetAllBranchOfUser(userId);
                var mBranchs = branchIds.Select(p => new MessengerBranch
                {
                    MessengerBranchId = Guid.NewGuid(),
                    BranchId = p.BranchId,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                });
                //commit
                var ret = await this._unitOfWork.Messengers.Save(m, mBranchs.ToArray(), value.MessengerId);

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
