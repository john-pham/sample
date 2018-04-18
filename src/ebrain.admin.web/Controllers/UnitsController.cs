﻿// ======================================
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
    [Security("")]
    public class UnitsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public UnitsController(IUnitOfWork unitOfWork, ILogger<UnitsController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<UnitViewModel>> Search(string filter, string value)
        {
            var userId = Utilities.GetUserId(this.User);
            var ret = from c in await this._unitOfWork.Units.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new UnitViewModel
                      {
                          ID = c.UnitId,
                          Code = c.UnitCode,
                          Name = c.UnitName,
                          Note = c.Note
                      };

            return ret;
        }

        [HttpGet("getall")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<UnitViewModel>> GetAll()
        {
            var ret = from c in await this._unitOfWork.Units.GetAllUnits(this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new UnitViewModel
                      {
                          ID = c.UnitId,
                          Code = c.UnitCode,
                          Name = c.UnitName,
                          Note = c.Note
                      };

            return ret;
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<UnitViewModel> Get(Guid? index)
        {
            var c = await this._unitOfWork.Units.FindById(index);

            return new UnitViewModel
            {
                ID = c.UnitId,
                Code = c.UnitCode,
                Name = c.UnitName,
                Note = c.Note
            }; ;
        }

        private Guid userId
        {
            get
            {
                return Utilities.GetUserId(this.User);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UnitViewModel value)
        {
            if (ModelState.IsValid)
            {
               var ret = await this._unitOfWork.Units.Save(new Unit
                {
                    UnitId = Guid.NewGuid(),
                    UnitCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    UnitName = value.Name,
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

                var ret = await this._unitOfWork.Units.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }
    }
}
