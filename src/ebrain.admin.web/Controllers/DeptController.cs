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
        public IEnumerable<DeptViewModel> GetDeptList(string filter, string value, string fromDate, string toDate)
        {
            var userId = Utilities.GetUserId(this.User);

            var results = this._unitOfWork.Depts.GetDeptList(
                fromDate.BuildDateTimeFromSEFormat(),
                toDate.BuildLastDateTimeFromSEFormat(),
                value,
                this._unitOfWork.Branches.GetAllBranchOfUserString(userId));
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
            return list;
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
