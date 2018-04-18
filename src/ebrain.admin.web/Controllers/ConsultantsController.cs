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
using AutoMapper;
using ebrain.admin.bc.Models;
using Microsoft.Extensions.Logging;
using Ebrain.Helpers;
using Microsoft.AspNetCore.Authorization;
using Ebrain.Policies;

namespace Ebrain.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Security("8AA6E971-1C3D-4835-B154-D662CE12AE97")]
    public class ConsultantsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public ConsultantsController(IUnitOfWork unitOfWork, ILogger<ConsultantsController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        private Guid userId
        {
            get
            {
                return Utilities.GetUserId(this.User);
            }
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<ConsultantViewModel>> Search(string filter, string value)
        {
            var ret = from c in await this._unitOfWork.Consultants.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new ConsultantViewModel
                      {
                          ID = c.ConsultantId,
                          Code = c.ConsultantCode,
                          Name = c.ConsultantName,
                          Note = c.Note
                      };

            return ret;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] ConsultantViewModel value)
        {
            if (ModelState.IsValid)
            {
                var userId = Utilities.GetUserId(this.User);

                var ret = await this._unitOfWork.Consultants.Save(new Consultant
                {
                    ConsultantId = Guid.NewGuid(),
                    ConsultantCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    ConsultantName = value.Name,
                    Note = value.Note,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }, value.ID);

                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] String id)
        {
            if (ModelState.IsValid)
            {
                var userId = Utilities.GetUserId(this.User);

                var ret = await this._unitOfWork.Consultants.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<ConsultantViewModel> Get(Guid? index)
        {
            var c = await this._unitOfWork.Consultants.Get(index);

            return new ConsultantViewModel
            {
                ID = c.ConsultantId,
                Code = c.ConsultantCode,
                Name = c.ConsultantName,
                Note = c.Note
            };
        }
    }
}
