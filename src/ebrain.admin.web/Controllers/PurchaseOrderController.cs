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
    public class PurchaseOrderController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;

        public PurchaseOrderController(IUnitOfWork unitOfWork, ILogger<IOStudentController> logger) : base(unitOfWork, logger)
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


        [HttpGet("getdefault")]
        [Produces(typeof(UserViewModel))]
        public Task<IActionResult> GetDefault(Guid? index)
        {
            return GetDefaultMain(index);
        }

        private async Task<IActionResult> GetDefaultMain(Guid? index)
        {
            if (index.IsNullOrDefault())
            {
                var ioNumber = this._unitOfWork.ConfigNumberOfCodes.GenerateCodePurchaseOrder(userId.ToString());

                var itemNew = new PurchaseOrderViewModel
                {
                    Code = ioNumber,
                    CreateDate = DateTime.Now,
                    CreateBy = userId,
                    IODetails = new PurchaseOrderDetailViewModel[0]
                };
                return Ok(itemNew);
            }
            return await GetPurchaseOrder(index);
        }

        private async Task<IActionResult> GetPurchaseOrder(Guid? id)
        {
            var ret = await this._unitOfWork.PurchaseOrders.FindById(id);
            if (ret != null)
            {
                var iods = await this._unitOfWork.PurchaseOrders.GetDetailByIOId(ret.PurchaseOrderId);
                var iodNews = new List<PurchaseOrderDetailViewModel>();
                foreach (var item in iods)
                {
                    var itemMate = await this._unitOfWork.Materials.Get(item.MaterialId);
                    var itemGrpMate = await this._unitOfWork.GrpMaterials.FindById(itemMate.GrpMaterialId);
                    var itemType = await this._unitOfWork.TypeMaterials.FindById(itemGrpMate.TypeMaterialId);
                    iodNews.Add(new PurchaseOrderDetailViewModel
                    {
                        ID = item.PurchaseOrderDetailId,
                        MaterialId = item.MaterialId,
                        MaterialCode = itemMate.MaterialCode,
                        MaterialName = itemMate.MaterialName,
                        GrpMaterial = itemGrpMate != null ? itemGrpMate.GrpMaterialName : string.Empty,
                        TypeMaterial = itemType != null ? itemType.TypeMaterialName : string.Empty,
                        Quantity = item.InputQuantity,
                    });

                }
                return Ok(new PurchaseOrderViewModel
                {
                    ID = ret.PurchaseOrderId,
                    Code = ret.PurchaseOrderCode,
                    SupplierId = ret.SupplierId,
                    Note = ret.Note,
                    CreateBy = ret.CreatedBy,
                    CreateDate = ret.CreatedDate,
                    IODetails = iodNews.ToArray()
                });
            }
            return BadRequest(ModelState);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] PurchaseOrderViewModel value)
        {
            if (ModelState.IsValid)
            {
                var ioId = Guid.NewGuid();
                var io = new PurchaseOrder
                {
                    PurchaseOrderId = ioId,
                    PurchaseOrderCode = value.Code,
                    SupplierId = value.SupplierId,
                    CreatedBy = userId,
                    CreatedDate = value.CreateDate,
                    UpdatedBy = userId,
                    UpdatedDate = DateTime.Now,
                    BranchId = ioId,
                    Note = value.Note
                };
                var ioDetails = value.IODetails.Select(p => new PurchaseOrderDetail
                {
                    PurchaseOrderDetailId = p.ID == null ? Guid.NewGuid() : p.ID.Value,
                    PurchaseOrderId = ioId,
                    MaterialId = p.MaterialId,
                    MaterialCode = p.MaterialCode,
                    InputQuantity = p.Quantity,
                    PriceBeforeVAT = p.SellPrice,
                    PriceAfterVAT = p.SellPrice,
                    TotalPrice = p.SellPrice * p.Quantity,
                    TotalPriceBeforeVAT = p.SellPrice * p.Quantity,
                    VAT = 0,
                    CreatedBy = userId,
                    CreatedDate = value.CreateDate,
                    UpdatedBy = userId,
                    UpdatedDate = DateTime.Now

                });

                io.TotalPrice = ioDetails.Sum(p => p.TotalPrice);
                io.TotalPriceBeforeVAT = ioDetails.Sum(p => p.TotalPriceBeforeVAT);

                var ret = await this._unitOfWork.PurchaseOrders.Save(io, ioDetails.ToArray(), value.ID);

                return await GetPurchaseOrder(ret.PurchaseOrderId);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("deletemaster")]
        public async Task<IActionResult> DeleteMaster([FromBody] Guid? id)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.PurchaseOrders.DeleteMaster(id);
                return await GetDefault(null);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelMaster([FromBody] Guid? id)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.PurchaseOrders.CancelMaster(id);
                return await GetDefault(null);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] String id)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.Units.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("getpurchaseorders")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> GetPurchaseOrderList(string filter, string value, string fromDate, string toDate, int page, int size)
        {
            var unit = this._unitOfWork.PurchaseOrders;
            var results = unit.GetPurchaseOrderList(
                            fromDate.BuildDateTimeFromSEFormat(),
                            toDate.BuildLastDateTimeFromSEFormat(),
                            value,
                            this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                            page, size
                            );
            var list = new List<PurchaseOrderViewModel>();
            foreach (var item in results)
            {
                list.Add(new PurchaseOrderViewModel
                {
                    ID = item.PurchaseOrderId,
                    Code = item.PurchaseOrderCode,
                    FullName = item.FullName,
                    BranchName = item.BranchName,
                    CreateDate = item.CreatedDate,
                    PurchaseQuantity = item.PurchaseQuantity,
                    IOQuantity = item.IOQuantity,
                    RemainQuantity = item.PurchaseQuantity - item.IOQuantity,
                    Note = item.Note,
                });
            }

            return Json(new
            {
                Total = unit.Total,
                List = list
            });
        }
    }
}
