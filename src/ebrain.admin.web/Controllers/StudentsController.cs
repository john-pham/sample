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

namespace Ebrain.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Security("8AA6E971-1C3D-4835-B154-D662CE12AE98", "8AA6E971-1C3D-4835-B154-D662CE12AE14",
        "8AA6E971-1C3D-4835-B154-D662CE12AE99")]
    public class StudentsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public StudentsController(IUnitOfWork unitOfWork, ILogger<StudentsController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("getall")]
        [Produces(typeof(List<UserViewModel>))]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var students = await _unitOfWork.Students.GetAll(page, pageSize, this._unitOfWork.Branches.GetAllBranchOfUserString(userId));
            var list = await MappingMaterial(students);
            return Ok(list);
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<StudentViewModel>> Search(string filter, string value)
        {
            var students = await _unitOfWork.Students.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId));
            return await MappingMaterial(students);
        }

        private Guid userId
        {
            get
            {
                return Utilities.GetUserId(this.User);
            }
        }

        public async Task<IEnumerable<StudentViewModel>> MappingMaterial(IEnumerable<Student> students)
        {
            List<StudentViewModel> list = new List<StudentViewModel>();

            foreach (var item in students)
            {
                var itemNew = await ProcessMappingItem(item);
                list.Add(itemNew);
            }

            return list;
        }

        private async Task<StudentViewModel> ProcessMappingItem(Student item)
        {
            var itemStudentRe = await this._unitOfWork.Students.FindRelationShipByStudentId(item.StudentId);
            var itemNew = new StudentViewModel
            {
                ID = item.StudentId,
                Code = item.StudentCode,
                Name = item.StudentName,
                Note = item.Note,
                Address = item.Address,
                Taxcode = item.TaxCode,
                AccountBank = item.AccountBank,
                Email = item.Email,
                GenderId = item.GenderId,
                Username = item.UserName,
                Password = item.Password,
                SchoolName = item.SchoolName,
                ClassName = item.ClassName,
                Birthday = item.Birthday,
                StudentStatusId = item.StudentStatusId,
                Phone = item.Phone,
                FaUsername = itemStudentRe != null ? itemStudentRe.FullName : string.Empty,
                FaAddress = itemStudentRe != null ? itemStudentRe.Address : string.Empty,
                FaEmail = itemStudentRe != null ? itemStudentRe.Email : string.Empty,
                FaJob = itemStudentRe != null ? itemStudentRe.Job : string.Empty,
                FaRelationship = itemStudentRe != null ? itemStudentRe.RelationRequire : string.Empty,
                FaPhone = itemStudentRe != null ? itemStudentRe.Phone : string.Empty,
            };
            return itemNew;
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<StudentViewModel> Get(Guid? index)
        {
            var item = await this._unitOfWork.Students.Get(index);

            return await ProcessMappingItem(item);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] StudentViewModel value)
        {
            if (ModelState.IsValid)
            {
                var studentId = Guid.NewGuid();

                var ret = await this._unitOfWork.Students.Save(new Student
                {
                    StudentId = studentId,
                    StudentCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    StudentName = value.Name,
                    AccountBank = value.AccountBank,
                    Address = value.Address,
                    Birthday = value.Birthday,
                    ClassName = value.ClassName,
                    SchoolName = value.SchoolName,
                    Phone = value.Phone,
                    TaxCode = value.Taxcode,
                    Email = value.Email,
                    GenderId = value.GenderId,
                    StudentStatusId = value.StudentStatusId,

                    UserName = value.Username,
                    Password = value.Password,
                    Note = value.Note,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,

                }, new StudentRelationShip
                {
                    StudentRelationShipId = Guid.NewGuid(),
                    StudentId = studentId,
                    FullName = value.FaUsername,
                    Facebook = value.FaFacebook,
                    Address = value.FaAddress,
                    Email = value.FaEmail,
                    Job = value.FaJob,
                    Phone = value.FaPhone,
                    Birthday = DateTime.Now,//value.Birthday,
                    BranchId = Guid.NewGuid(),
                    RelationRequire = value.FaRelationship,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                }, value.ID);

                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("getbirthdaytudents")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> GetBirthdayStudent(string filter, string value, string fromDate, string toDate)
        {
            var unit = this._unitOfWork.Students;
            var results = unit.GetStudentBirthday
                        (
                            this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                            fromDate.BuildDateTimeFromSEFormat(),
                            toDate.BuildLastDateTimeFromSEFormat()
                        );
            var list = results.Select(p => new StudentViewModel
            {
                ID = p.StudentId,
                Code = p.StudentCode,
                Name = p.StudentName,
                Birthday = p.Birthday,
                Phone = p.Phone,
                Email = p.Email,
                GenderName = p.GenderName,
                TotalDay = p.TotalDay
            });


            return Json(new
            {
                Total = unit.Total,
                List = list
            });
        }

        [HttpGet("getstudentendclass")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> GetStudentEndClass(string filter, string value, string classId, string toDate)
        {
            var unit = this._unitOfWork.Students;
            var results = unit.GetStudentEndClass
                        (
                            this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                            classId,
                            toDate.BuildLastDateTimeFromSEFormat()
                        );
            var list = results.Select(p => new StudentViewModel
            {
                ID = p.StudentId,
                Code = p.StudentCode,
                Name = p.StudentName,
                Birthday = p.Birthday,
                ClassName = p.ClassName,
                ClassCode = p.ClassCode,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Phone = p.Phone,
                Email = p.Email,
                GenderName = p.GenderName,
                TotalDay = p.TotalDay
            });

            return Json(new
            {
                Total = unit.Total,
                List = list
            });
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] String id)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.Students.Delete(id);
                return Ok(ret);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("getstudentbycreatedate")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> GetStudentByCreateDate(string filter, string value, string fromDate, string toDate, int page, int size)
        {
            var list = GetStudentByCreateDateMain(filter, value, fromDate.BuildDateTimeFromSEFormat(), toDate.BuildLastDateTimeFromSEFormat(), page, size);
            return Json(new
            {
                Total = this._unitOfWork.Students.Total,
                List = list
            });

        }

        private IEnumerable<StudentViewModel> GetStudentByCreateDateMain(string filter, string value, DateTime? fromDate, DateTime? toDate, int page, int size)
        {
            // var results = await this._unitOfWork.IOStocks.Search(filter, value);
            var results = this._unitOfWork.Students.GetStudentByCreateDate
                        (
                            this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                            fromDate,
                            toDate,
                            page,
                            size
                        );
            return results.Select(p => new StudentViewModel
            {
                ID = p.StudentId,
                Code = p.StudentCode,
                Name = p.StudentName,
                Birthday = p.Birthday,
                Phone = p.Phone,
                Email = p.Email,
                GenderName = p.GenderName,
                TotalDay = p.TotalDay
            });

        }

        [HttpGet("getnewstudent")]
        [Produces(typeof(UserViewModel))]
        public IActionResult GetNewStudent()
        {
            var list = GetStudentByCreateDateMain(string.Empty, string.Empty, DateTime.Now, DateTime.Now, 0, 0);
            return Ok(list.Count());
        }

        [HttpGet("getalltudent")]
        [Produces(typeof(UserViewModel))]
        public IActionResult GetAllStudent()
        {
            var list = GetStudentByCreateDateMain(string.Empty, string.Empty, new DateTime(1900, 01, 01), DateTime.Now, 0, 0);
            return Ok(list.Count());
        }

        [HttpGet("getstudentcourse")]
        [Produces(typeof(UserViewModel))]
        public IActionResult GetStudentCourse(string filterValue, string studentId, int page, int size)
        {
            var unit = this._unitOfWork.Students;
            var results = unit.GetStudentCourse
                        (
                            filterValue,
                            studentId,
                            this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                            page,
                            size
                        );
            var list = results.Select(p => new StudentViewModel
            {
                ID = p.StudentId,
                Code = p.StudentCode,
                Name = p.StudentName,
                Birthday = p.Birthday,
                ClassName = p.ClassName,
                ClassCode = p.ClassCode,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Phone = p.Phone,
                Email = p.Email,
                GenderName = p.GenderName,
                TotalDay = p.TotalDay,
                MaterialName = p.MaterialName
            });


            return Json(new
            {
                Total = unit.Total,
                List = list
            });
        }
    }
}
