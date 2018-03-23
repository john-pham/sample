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
using ebrain.admin.bc.Utilities;
using ebrain.admin.bc.Report;

namespace Ebrain.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ClassesController : Controller
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public ClassesController(IUnitOfWork unitOfWork, ILogger<ClassesController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        private Guid userId
        {
            get
            {
                return new Guid(Utilities.GetUserId(this.User));
            }
        }


        [HttpGet("getdefault")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetDefaultMain(Guid? index)
        {
            if (index.IsNullOrDefault())
            {
                var ioNumber = this._unitOfWork.ConfigNumberOfCodes.GenerateCodeMain("Class", "", userId.ToString(), 3);

                var itemNew = new ClassViewModel
                {
                    Code = ioNumber,
                    Note = string.Empty
                };
                return Ok(itemNew);
            }
            return await Get(index);
        }

        [HttpGet("gettoday")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<TodayViewModel>> GetToday(string index)
        {
            var ret = from c in await this._unitOfWork.Today.GetAll(this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new TodayViewModel
                      {
                          ID = c.TodayId,
                          Code = c.TodayCode,
                          Name = c.TodayName,
                          Note = c.Note
                      };

            return ret;
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<ClassViewModel>> Search(string filter, string value)
        {
            var ret = await this._unitOfWork.Classes.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId));
            List<ClassViewModel> list = new List<ClassViewModel>();
            if (ret != null)
            {
                foreach (var c in ret)
                {
                    var item = new ClassViewModel();
                    var material = await this._unitOfWork.Materials.Get(c.MaterialId);
                    item.MaterialName = material != null ? material.MaterialName : string.Empty;
                    item.ID = c.ClassId;
                    item.Code = c.ClassCode;
                    item.Name = c.ClassName;
                    item.Note = c.Note;
                    item.StartDate = c.StartDate;
                    item.EndDate = c.EndDate;
                    list.Add(item);
                }

            }
            return list;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] ClassViewModel value)
        {
            if (ModelState.IsValid)
            {
                if (value.Times == null) value.Times = new ClassTimeViewModel[0];
                if (value.Students == null) value.Students = new ClassStudentViewModel[0];

                var ret = await this._unitOfWork.Classes.Save(new Class
                {
                    ClassId = Guid.NewGuid(),
                    ClassCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    ClassName = value.Name,
                    Note = value.Note,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    MaxStudent = value.MaxStudent,
                    EndDate = value.EndDate,
                    StartDate = value.StartDate,
                    StatusId = value.StatusId,
                    LongLearn = value.LongLearn,
                    MaterialId = value.MaterialId,
                    SupplierId = value.SupplierId,
                }, value.Times.Select(p => new ClassTime
                {
                    ClassId = p.ClassId,
                    ClassTimeId = p.ID,
                    RoomId = p.RoomId,
                    BranchId = p.BranchId,
                    EndTime = p.EndTime,
                    StartTime = p.StartTime,
                    SupplierId = p.SupplierId,
                    OnTodayId = p.OnTodayId,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                }).ToArray(),
                value.Students.Select(p => new ClassStudent
                {
                    StudentId = p.StudentId,
                    ClassStudentId = p.ID,
                    ClassId = p.ClassId,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                }).ToArray(), value.ID);

                return await Get(ret.ClassId);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] String id)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.Classes.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("getfirstclass")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetFirstClass(Guid? index)
        {
            var c = await this._unitOfWork.Classes.Get(index);
            var itemExist = new ClassViewModel();
            if (c != null)
            {
                var itemMate = await this._unitOfWork.Materials.Get(c.MaterialId);
                itemExist = new ClassViewModel
                {
                    ID = c.ClassId,
                    Code = c.ClassCode,
                    Name = c.ClassName,
                    Note = c.Note,
                    BranchId = c.BranchId,
                    EndDate = c.EndDate,
                    StartDate = c.StartDate,
                    LongLearn = c.LongLearn,
                    MaterialId = c.MaterialId,
                    MaxStudent = c.MaxStudent,
                    StatusId = c.StatusId,
                    SupplierId = c.SupplierId,
                    MaterialName = itemMate != null ? itemMate.MaterialName : string.Empty
                };
            }
            return Ok(itemExist);
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> Get(Guid? index)
        {
            var c = await this._unitOfWork.Classes.Get(index);
            var itemExist = new ClassViewModel();
            if (c != null)
            {
                itemExist = new ClassViewModel
                {
                    ID = c.ClassId,
                    Code = c.ClassCode,
                    Name = c.ClassName,
                    Note = c.Note,
                    BranchId = c.BranchId,
                    EndDate = c.EndDate,
                    StartDate = c.StartDate,
                    LongLearn = c.LongLearn,
                    MaterialId = c.MaterialId,
                    MaxStudent = c.MaxStudent,
                    StatusId = c.StatusId,
                    SupplierId = c.SupplierId
                };

                //get time from classId
                var itemClassTimses = await this._unitOfWork.ClassTimes.GetClassTimeFromClassId(index);
                if (itemClassTimses != null)
                {
                    //get times
                    itemExist.Times = itemClassTimses.Select(p => new ClassTimeViewModel
                    {
                        ClassId = p.ClassId,
                        ID = p.ClassTimeId,
                        RoomId = p.RoomId,
                        BranchId = p.BranchId,
                        EndTime = p.EndTime,
                        StartTime = p.StartTime,
                        SupplierId = p.SupplierId,
                        OnTodayId = p.OnTodayId,

                    }).ToArray();
                }

                //get student from classId
                var itemClassStudents = await this._unitOfWork.ClassStudents.GetClassStudentFromClassId(index);
                if (itemClassStudents != null)
                {
                    itemExist.Students = itemClassStudents.Select(p => new ClassStudentViewModel
                    {
                        StudentId = p.StudentId,
                        ID = p.ClassStudentId,
                        ClassId = p.ClassId
                    }).ToArray();
                }
            }
            return Ok(itemExist);
        }

        private IEnumerable<ClassViewModel> MappingClassViewModel(List<ClassList> list)
        {
            if (list != null && list.Count > 0)
            {
                return list.Select(p => new ClassViewModel
                {
                    Code = p.ClassCode,
                    Name = p.ClassName,
                    ID = p.ClassId,
                    LongLearn = p.LongLearn,
                    MaxStudent = p.MaxStudent,
                    StatusId = p.StatusId,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    SupplierName = p.SupplierName,
                    Address = p.Address,
                    FullName = p.FullName,
                    MaterialName = p.MaterialName,
                    CreatedDate = p.CreatedDate,
                    CountStudent = p.CountStudent
                });
            }
            return null;
        }

        [HttpGet("getclasses")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetClasses(string filter, string value, Guid? statusId, Guid? supplierId)
        {
            var list = this._unitOfWork.Classes.GetClasses(
                    this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                    value,
                    statusId,
                    supplierId
                    );
            if (list != null && list.Count > 0)
            {
                return Ok(MappingClassViewModel(list));
            }
            return Ok(null);
        }

        [HttpGet("getsummaries")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetSummaries(string filter, string value, Guid? statusId, Guid? supplierId, Guid? classId)
        {
            var list = this._unitOfWork.Classes.GetClassSummary(
                    this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                   value,
                    statusId,
                    supplierId,
                    classId);
            if (list != null && list.Count > 0)
            {
                return Ok(MappingClassViewModel(list));
            }
            return Ok(null);
        }

        [HttpGet("getclassbystudentid")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetClassByStudentId(string filter, string value, Guid? statusId, Guid? supplierId, Guid? classId, Guid? studentId)
        {
            var list = this._unitOfWork.Classes.GetClassStudent(
                    this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                    value,
                    statusId,
                    supplierId,
                    classId,
                    studentId);
            if (list != null && list.Count > 0)
            {
                return Ok(MappingClassViewModel(list));
            }
            return Ok(null);
        }

        [HttpGet("getclassexamine")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetClassExamine(Guid? classId, Guid? studentId)
        {
            var list = this._unitOfWork.Classes.GetClassExamine(
                    this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                    classId,
                    studentId);
            if (list != null && list.Count > 0)
            {
                return Ok(list.Select(p => new ClassExamineViewModel
                {
                    ExamineId = p.ExamineId,
                    ExamineCode = p.ExamineCode,
                    ExamineName = p.ExamineName,
                    Mark = p.Mark,
                    StudentId = p.StudentId,
                    ClassId = p.ClassId
                }));
            }
            return Ok(null);
        }

        [HttpPost("updateclassexamine")]
        public IActionResult SaveClassExamine([FromBody]ClassExamineViewModel[] examines)
        {
            if (ModelState.IsValid)
            {
                this._unitOfWork.Classes.SaveClassExamine(examines.Select(p => new ClassExamine
                {
                    ClassExamineId = Guid.NewGuid(),
                    ExamineId = p.ExamineId,
                    ClassId = p.ClassId,
                    StudentId = p.StudentId,
                    Mark = p.Mark,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = userId,
                    UpdatedDate = DateTime.Now
                }).ToArray());
                return Ok(new ClassExamineViewModel());
            }
            return Ok(null);
        }

        [HttpPost("updatestudent")]
        public async Task<IActionResult> UpdateClassStudent([FromBody] List<ClassViewModel> values)
        {
            if (ModelState.IsValid && values != null && values.Count > 0)
            {
                var studentId = values[0].StudentId;
                this._unitOfWork.Classes.SaveStudent(values.Select(p => new Class
                {
                    ClassId = p.ID.HasValue ? p.ID.Value : Guid.Empty
                }).ToArray(), studentId, userId,
                this._unitOfWork.Branches.GetAllBranchOfUserString(userId));
                return Ok(new Class());
            }
            return Ok(null);
        }
    }
}
