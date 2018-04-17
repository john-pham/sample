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
    public class ShiftClassesController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public ShiftClassesController(IUnitOfWork unitOfWork, ILogger<ShiftClassesController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<ShiftClassViewModel>> Search(string filter, string value)
        {
            var ret = from c in await this._unitOfWork.ShiftClasses.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new ShiftClassViewModel
                      {
                          ID = c.ShiftClassId,
                          Code = c.ShiftClassCode,
                          Name = c.ShiftClassName,
                          Note = c.Note
                      };

            return ret;
        }

        private Guid userId
        {
            get
            {
                return Utilities.GetUserId(this.User);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] ShiftClassViewModel value)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.ShiftClasses.Save(new ShiftClass
                {
                    ShiftClassId = Guid.NewGuid(),
                    ShiftClassCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    ShiftClassName = value.Name,
                    Note = value.Note,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
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
                var ret = await this._unitOfWork.ShiftClasses.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<ShiftClassViewModel> Get(Guid? index)
        {
            var c = await this._unitOfWork.ShiftClasses.Get(index);

            return new ShiftClassViewModel
            {
                ID = c.ShiftClassId,
                Code = c.ShiftClassCode,
                Name = c.ShiftClassName,
                Note = c.Note
            };
        }
    }
}
