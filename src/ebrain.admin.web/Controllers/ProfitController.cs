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
    [Security("376F3FFE-408A-49A8-B0EA-B69654E11B36", "376F3FFE-408A-49A8-B0EA-B69654E11B38")]
    public class ProfitController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;

        public ProfitController(IUnitOfWork unitOfWork, ILogger<ProfitController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("getprofits")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> GetProfitList(string filter, string value, string fromDate, string toDate, int page, int size)
        {
            var userId = Utilities.GetUserId(this.User);
            var unit = this._unitOfWork.Profits;
            var results = unit.GetProfitList(
                fromDate.BuildDateTimeFromSEFormat(),
                toDate.BuildLastDateTimeFromSEFormat(),
                value,
                this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                page, size);

            var list = new List<ProfitViewModel>();
            foreach (var item in results)
            {
                list.Add(new ProfitViewModel
                {
                    ID = item.BranchId,
                    Code = item.BranchCode,
                    Name = item.BranchName,
                    TotalPriceFirst = item.TotalPriceFirst,
                    TotalPricePayment = item.TotalPricePayment,
                    TotalPriceReceipt = item.TotalPriceReceipt,
                    TotalPriceEnd = item.TotalPriceEnd,
                });
            }

            return Json(new
            {
                Total = unit.Total,
                List = list
            });
        }

        [HttpGet("updatedprofits")]
        [Produces(typeof(UserViewModel))]
        public Task<bool> UpdateInventories(string filter, string value, string fromDate, string toDate)
        {
            var userId = Utilities.GetUserId(this.User);

            var results = this._unitOfWork.Profits.UpdateProfit(
                fromDate.BuildDateTimeFromSEFormat(),
                toDate.BuildLastDateTimeFromSEFormat(),
                value,
                this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                userId);

            return results;
        }
    }
}
