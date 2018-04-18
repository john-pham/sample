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
    public class StudentStatusController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public StudentStatusController(IUnitOfWork unitOfWork, ILogger<StudentStatusController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<StudentStatusViewModel>> Search(string filter, string value)
        {
            var ret = from c in await this._unitOfWork.StudentStatus.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new StudentStatusViewModel
                      {
                          ID = c.StudentStatusId,
                          Code = c.StudentStatusCode,
                          Name = c.StudentStatusName,
                          Note = c.Note
                      };

            return ret;
        }

        [HttpGet("getall")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<StudentStatusViewModel>> GetAll()
        {
            var ret = from c in await this._unitOfWork.StudentStatus.GetAllStudentStatus(this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new StudentStatusViewModel
                      {
                          ID = c.StudentStatusId,
                          Code = c.StudentStatusCode,
                          Name = c.StudentStatusName,
                          Note = c.Note
                      };

            return ret;
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<StudentStatusViewModel> Get(Guid? index)
        {
            var c = await this._unitOfWork.StudentStatus.FindById(index);

            return new StudentStatusViewModel
            {
                ID = c.StudentStatusId,
                Code = c.StudentStatusCode,
                Name = c.StudentStatusName,
                Note = c.Note
            }; 
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] StudentStatusViewModel value)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.StudentStatus.Save(new StudentStatus
                {
                    StudentStatusId = Guid.NewGuid(),
                    StudentStatusCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    StudentStatusName = value.Name,
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

        private Guid userId
        {
            get
            {
                return Utilities.GetUserId(this.User);
            }
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] String id)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.StudentStatus.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }
    }
}
