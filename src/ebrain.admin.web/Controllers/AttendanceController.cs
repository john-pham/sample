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
using ebrain.admin.bc.Utilities;

namespace Ebrain.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Security("")]
    public class AttendanceController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public AttendanceController(IUnitOfWork unitOfWork, ILogger<AttendanceController> logger) : base(unitOfWork, logger)
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
        public async Task<IEnumerable<AttendanceViewModel>> Search(string classId, string studentId, string createDate)
        {
            return await SearchMain(classId, studentId, createDate.BuildDateTimeFromSEFormat());
        }

        private async Task<IEnumerable<AttendanceViewModel>> SearchMain(string classId, string studentId, DateTime createDate)
        {
            var ret = from c in await this._unitOfWork.Attendances.Search(
                    classId,
                    studentId,
                    createDate,
                    this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new AttendanceViewModel
                      {
                          ClassId = c.ClassId,
                          StudentCode = c.StudentCode,
                          StudentId = c.StudentId,
                          StudentName = c.StudentName,
                          AttendanceDate = createDate,
                          Absent = c.Absent,
                          NotAbsent = !c.Absent,
                          BranchId = c.BranchId,
                          Phone = c.Phone
                      };

            return ret;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] AttendanceViewModel[] values)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.Attendances.Save(values.Select(p => new Attendance
                {
                    AttendanceId = Guid.NewGuid(),
                    ClassId = p.ClassId,
                    StudentId = p.StudentId,
                    Note = string.Empty,
                    AttendanceDate = p.AttendanceDate,
                    Absent = p.Absent,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }).ToArray(), userId);

                if (values != null && values.Length > 0)
                {
                    var classId = values[0].ClassId.ToString();
                    var createDate = values[0].AttendanceDate;
                    return Ok(SearchMain(classId, string.Empty, createDate));
                }
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

    }
}
