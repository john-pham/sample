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
using ebrain.admin.bc.Core.Interfaces;
using ebrain.admin.bc.Utilities;

namespace Ebrain.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Security("376F3FFE-408A-49A8-B0EA-B69654E11B37", "376F3FFE-408A-49A8-B0EA-B69654E11B35")]
    public class DeptController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;

        public DeptController(IUnitOfWork unitOfWork, ILogger<DeptController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("getdepts")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> GetDeptList(string filter, string value, string fromDate, string toDate, int page, int size)
        {
            var userId = Utilities.GetUserId(this.User);
            var unit = this._unitOfWork.Depts;
            var results = unit.GetDeptList(
                fromDate.BuildDateTimeFromSEFormat(),
                toDate.BuildLastDateTimeFromSEFormat(),
                value,
                this._unitOfWork.Branches.GetAllBranchOfUserString(userId), page, size);
            var list = new List<DeptViewModel>();
            foreach (var item in results)
            {
                list.Add(new DeptViewModel
                {
                    StudentId = item.StudentId,
                    StudentCode = item.StudentCode,
                    StudentName = item.StudentName,
                    Phone = item.Phone,
                    Receipt = item.Receipt,
                    Payment = item.Payment,
                    ReceiptFirst = item.ReceiptFirst,
                    PaymentFirst = item.PaymentFirst,
                    TotalPricePayment = item.TotalPricePayment,
                    TotalPriceReceipt = item.TotalPriceReceipt,
                    EndPayment = item.EndPayment,
                    EndReceipt = item.EndReceipt,

                });
            }

            return Json(new
            {
                Total = unit.Total,
                List = list
            });

        }

        [HttpGet("updateddepts")]
        [Produces(typeof(UserViewModel))]
        public Task<bool> UpdateDept(string filter, string value, string fromDate, string toDate)
        {
            var userId = Utilities.GetUserId(this.User);

            var results = this._unitOfWork.Depts.UpdateDept(
                fromDate.BuildDateTimeFromSEFormat(),
                toDate.BuildLastDateTimeFromSEFormat(),
                value,
                this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                userId);

            return results;
        }
    }
}
