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
    [Security("9ECBD467-7642-467B-AAE2-96484AD182A8")]
    public class InventoriesController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;

        public InventoriesController(IUnitOfWork unitOfWork, ILogger<InventoriesController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("getinventories")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> GetInventoryList(string filter, string value, string fromDate, string toDate, int page, int size)
        {
            var userId = Utilities.GetUserId(this.User);
            var unit = this._unitOfWork.Inventories;
            var results = unit.GetInventoryList(
                fromDate.BuildDateTimeFromSEFormat(),
                toDate.BuildLastDateTimeFromSEFormat(),
                value,
                this._unitOfWork.Branches.GetAllBranchOfUserString(userId), page, size);
            var list = new List<InventoriesViewModel>();
            foreach (var item in results)
            {
                list.Add(new InventoriesViewModel
                {
                    ID = item.MaterialId,
                    Code = item.MaterialCode,
                    Name = item.MaterialName,
                    TypeCode = item.TypeMaterialCode,
                    GrpCode = item.GrpMaterialCode,
                    TypeName = item.TypeMaterialName,
                    GrpName = item.GrpMaterialName,
                    QuantityInv = item.QuantityInv,
                    QuantityInput = item.QuantityInput,
                    QuantityOutput = item.QuantityOutput,
                    QuantityEnd = item.QuantityEnd
                });
            }

            return Json(new
            {
                Total = unit.Total,
                List = list
            });
        }

        [HttpGet("updatedinventories")]
        [Produces(typeof(UserViewModel))]
        public Task<bool> UpdateInventories(string filter, string value, string fromDate, string toDate)
        {
            var userId = Utilities.GetUserId(this.User);

            var results = this._unitOfWork.Inventories.UpdateInventory(
                fromDate.BuildDateTimeFromSEFormat(),
                toDate.BuildLastDateTimeFromSEFormat(),
                value,
                this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                userId);

            return results;
        }
    }
}
