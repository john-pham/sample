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
    [Security("8AA6E971-1C3D-4835-B154-D662CE12AE99", "8AA6E971-1C3D-4835-B154-D662CE12AE12")]
    public class IOStudentController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;

        public IOStudentController(IUnitOfWork unitOfWork, ILogger<IOStudentController> logger) : base(unitOfWork, logger)
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
        public async Task<JsonResult> Search(string filter, string value, string fromDate, string toDate, int page, int size)
        {
            var unit = this._unitOfWork.IOStocks;
            var list = await SearchMain(filter, value, fromDate, toDate, page, size);
            return Json(new
            {
                Total = unit.Total,
                List = list
            });
        }

        [HttpGet("reportsearch")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> ReportPurchaseOrderList(string filter, string value, string fromDate, string toDate, int allData, int page, int size)
        {

            var list = await SearchMain(filter, value, fromDate, toDate, 0, 0);
            var item = new ChartViewModel();
            if (list != null && list.Count > 0)
            {
                //sort
                var temps = list.GroupBy(p => new { p.BranchName, p.CreateDate_MMYY }).Select(p => new
                {
                    BranchName = p.Key.BranchName,
                    CreateDate = p.Key.CreateDate_MMYY,
                    TotalPrice = p.Sum(c => c.TotalPrice)

                }).ToList();
                item.ChartModels.AddRange(temps.GroupBy(p => p.BranchName).Select(p => new ChartModel
                {
                    Label = p.Key,
                    Data = p.Select(c => c.TotalPrice).ToArray()
                }));

                item.ChartLabels = temps.Select(p => p.CreateDate).ToArray();
            }

            return this.Ok(item);
        }

        public async Task<List<IOStockViewModel>> SearchMain(string filter, string value, string fromDate, string toDate, int page, int size)
        {
            var unit = this._unitOfWork.IOStocks;
            var results = unit.GetIOStockList(
                            fromDate.BuildDateTimeFromSEFormat(),
                            toDate.BuildLastDateTimeFromSEFormat(),
                            value, (int)EnumIOType.IORegisCourse,
                            this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                            page, size);
            var list = new List<IOStockViewModel>();
            foreach (var item in results)
            {
                list.Add(new IOStockViewModel
                {
                    ID = item.IOStockId,
                    Code = item.IONumber,
                    FullName = item.FullName,
                    StudentName = item.StudentName,
                    CreateDate = item.CreatedDate,
                    TotalPrice = item.TotalPrice,
                    IOTypeId = (int)item.IOTypeId,
                    Note = item.Note,
                    StudentId = item.StudentId,
                    BranchName  = item.BranchName
                });
            }
            return list;
        }


        [HttpGet("getdefault")]
        [Produces(typeof(UserViewModel))]
        public Task<IActionResult> GetDefault(Guid? index)
        {
            return GetDefaultMain(index, (int)EnumIOType.IORegisCourse);
        }

        [HttpGet("getdefaultinput")]
        [Produces(typeof(UserViewModel))]
        public Task<IActionResult> GetDefaultInput(Guid? index)
        {
            return GetDefaultMain(index, (int)EnumIOType.IOInput);
        }

        private async Task<IActionResult> GetDefaultMain(Guid? index, int ioTypeId)
        {
            if (index.IsNullOrDefault())
            {
                var itemNew = GenerateDefault(ioTypeId);

                return Ok(itemNew);
            }
            return await GetIOStock(index);
        }

        private IOStockViewModel GenerateDefault(int ioTypeId)
        {
            var ioNumber = this._unitOfWork.ConfigNumberOfCodes.GenerateCode(ioTypeId, userId.ToString());

            var itemNew = new IOStockViewModel
            {
                Code = ioNumber,
                CreateDate = DateTime.Now,
                CreateBy = userId,
                IOTypeId = ioTypeId,
                IODetails = new IOStockDetailViewModel[0]
            };
            return itemNew;
        }

        private async Task<IActionResult> GetIOStock(Guid? id)
        {
            var ret = await this._unitOfWork.IOStocks.FindById(id);
            if (ret != null)
            {
                var iods = await this._unitOfWork.IOStocks.GetDetailByIOId(ret.IOStockId);
                var iodNews = new List<IOStockDetailViewModel>();
                foreach (var item in iods)
                {
                    var itemMate = await this._unitOfWork.Materials.Get(item.MaterialId);
                    var itemGrpMate = await this._unitOfWork.GrpMaterials.FindById(itemMate.GrpMaterialId);
                    var itemType = await this._unitOfWork.TypeMaterials.FindById(itemGrpMate.TypeMaterialId);
                    iodNews.Add(new IOStockDetailViewModel
                    {
                        ID = item.IOStockDetailId,
                        MaterialId = item.MaterialId,
                        MaterialCode = itemMate.MaterialCode,
                        MaterialName = itemMate.MaterialName,
                        GrpMaterial = itemGrpMate != null ? itemGrpMate.GrpMaterialName : string.Empty,
                        TypeMaterial = itemType != null ? itemType.TypeMaterialName : string.Empty,
                        Quantity = item.InputQuantity,
                        SellPrice = item.PriceBeforeVAT,
                        TotalPrice = item.TotalPrice,
                        PurchaseOrderId = item.PurchaseOrderId,
                        PurchaseOrderDetailId = item.PurchaseOrderDetailId
                    });

                }
                return Ok(new IOStockViewModel
                {
                    ID = ret.IOStockId,
                    Code = ret.IONumber,
                    StudentId = ret.StudentId,
                    Note = ret.Note,
                    CreateBy = ret.CreatedBy,
                    CreateDate = ret.CreatedDate,
                    IOTypeId = ret.IOTypeId,
                    PurchaseOrderId = ret.PurchaseOrderId,
                    PurchaseOrderCode = ret.PurchaseOrderCode,
                    IODetails = iodNews.ToArray()
                });
            }
            return BadRequest(ModelState);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] IOStockViewModel value)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(value.PurchaseOrderCode))
                {
                    //check purchaseOrder
                    var results = this._unitOfWork.PurchaseOrders.GetPurchaseOrderListDetail(
                                  ObjectHelper.GetDateMin, ObjectHelper.GetDateNow,
                                   value.PurchaseOrderId.ToString(),
                                   this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                                   value.ID.HasValue ? value.ID.Value.ToString() : string.Empty,
                                   0, 0
                               );
                    var iods = value.IODetails;
                    foreach (var item in results)
                    {
                        var itemExist = iods.FirstOrDefault(p => p.PurchaseOrderDetailId == item.PurchaseOrderDetailId);
                        if (itemExist != null)
                        {
                            if ((item.PurchaseQuantity - item.IOQuantity) >= itemExist.Quantity) continue;
                            else throw new Exception("Đã hết đơn đặt hàng");
                        }
                        else
                        {
                            throw new Exception("Not exist purchase order");
                        }
                    }


                }
                var ioId = Guid.NewGuid();
                var io = new IOStock
                {
                    IOStockId = ioId,
                    IONumber = value.Code,
                    SupplierId = value.SupplierId,
                    CreatedBy = userId,
                    CreatedDate = value.CreateDate,
                    UpdatedBy = userId,
                    UpdatedDate = DateTime.Now,
                    IOTypeId = value.IOTypeId,
                    BranchId = ioId,
                    StudentId = value.StudentId,
                    Note = value.Note,
                    PurchaseOrderCode = value.PurchaseOrderCode,
                    PurchaseOrderId = value.PurchaseOrderId
                };
                var ioDetails = value.IODetails.Select(p => new IOStockDetail
                {
                    IOStockDetailId = p.ID == null ? Guid.NewGuid() : p.ID.Value,
                    IOStockId = ioId,
                    MaterialId = p.MaterialId,
                    MaterialCode = p.MaterialCode,
                    PriceBeforeVAT = p.SellPrice,
                    PriceAfterVAT = p.SellPrice,
                    VAT = 0,
                    InputQuantity = p.Quantity,
                    TotalPrice = p.SellPrice * p.Quantity,
                    TotalPriceBeforeVAT = p.SellPrice * p.Quantity,
                    CreatedBy = userId,
                    CreatedDate = value.CreateDate,
                    UpdatedBy = userId,
                    UpdatedDate = DateTime.Now,
                    PurchaseOrderDetailId = p.PurchaseOrderDetailId,
                    PurchaseOrderId = p.PurchaseOrderId

                });

                io.TotalPrice = ioDetails.Sum(p => p.TotalPrice);
                io.TotalPriceBeforeVAT = ioDetails.Sum(p => p.TotalPriceBeforeVAT);

                var ret = await this._unitOfWork.IOStocks.Save(io, ioDetails.ToArray(), value.ID);

                return await GetIOStock(ret.IOStockId);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("deletemaster")]
        public async Task<IActionResult> DeleteMaster([FromBody] Guid? id)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.IOStocks.DeleteMaster(id);
                return await GetDefault(null);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelMaster([FromBody] Guid? id)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.IOStocks.CancelMaster(id);
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
    }
}
