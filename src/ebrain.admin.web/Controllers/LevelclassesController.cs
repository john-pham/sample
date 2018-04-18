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
    [Security("")]
    public class LevelClassesController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public LevelClassesController(IUnitOfWork unitOfWork, ILogger<LevelClassesController> logger) : base(unitOfWork, logger)
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
        public async Task<IEnumerable<LevelclassViewModel>> Search(string filter, string value)
        {
            var ret = from c in await this._unitOfWork.LevelClasses.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new LevelclassViewModel
                      {
                          ID = c.LevelClassId,
                          Code = c.LevelClassCode,
                          Name = c.LevelClassName,
                          Note = c.Note
                      };

            return ret;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] LevelclassViewModel value)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.LevelClasses.Save(new LevelClass
                {
                    LevelClassId = Guid.NewGuid(),
                    LevelClassCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    LevelClassName = value.Name,
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
                var ret = await this._unitOfWork.LevelClasses.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<LevelclassViewModel> Get(Guid? index)
        {
            var c = await this._unitOfWork.LevelClasses.Get(index);

            return new LevelclassViewModel
            {
                ID = c.LevelClassId,
                Code = c.LevelClassCode,
                Name = c.LevelClassName,
                Note = c.Note
            };
        }
    }
}
