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
    public class GenderStudentsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public GenderStudentsController(IUnitOfWork unitOfWork, ILogger<GenderStudentsController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<GenderViewModel>> Search(string filter, string value)
        {
            var ret = from c in await this._unitOfWork.Genders.Search(filter, value)
                      select new GenderViewModel
                      {
                          ID = c.GenderId,
                          Code = c.GenderCode,
                          Name = c.GenderName,
                          Note = c.Note
                      };

            return ret;
        }

        [HttpGet("getall")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<GenderViewModel>> GetAll()
        {
            var ret = from c in await this._unitOfWork.Genders.GetAllGender()
                      select new GenderViewModel
                      {
                          ID = c.GenderId,
                          Code = c.GenderCode,
                          Name = c.GenderName,
                          Note = c.Note
                      };

            return ret;
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<GenderViewModel> Get(long? index)
        {
            var c = await this._unitOfWork.Genders.FindById(index);

            return new GenderViewModel
            {
                ID = c.GenderId,
                Code = c.GenderCode,
                Name = c.GenderName,
                Note = c.Note
            }; 
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] GenderViewModel value)
        {
            if (ModelState.IsValid)
            {
                var userId = Utilities.GetUserId(this.User);

                var ret = await this._unitOfWork.Genders.Save(new Gender
                {
                    GenderId = 1,
                    GenderCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    GenderName = value.Name,
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
        public async Task<IActionResult> Remove([FromBody] long id)
        {
            if (ModelState.IsValid)
            {
                var userId = Utilities.GetUserId(this.User);

                var ret = await this._unitOfWork.Genders.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }
    }
}
