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
                        SellPrice = item.PriceBeforeVAT,
                        TotalPrice = item.TotalPrice,
                        Note = item.Note
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
        public async Task<JsonResult> GetPurchaseOrderList(string filter, string value, string fromDate, string toDate, int allData, int page, int size)
        {

            var list = await GetPurchaseOrderListMain(filter, value, fromDate, toDate, allData, page, size);

            return Json(new
            {
                Total = this._unitOfWork.PurchaseOrders.Total,
                List = list
            });
        }

        [HttpGet("reportpurchaseorders")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> ReportPurchaseOrderList(string filter, string value, string fromDate, string toDate, int allData, int page, int size)
        {

            var list = await GetPurchaseOrderListMain(filter, value, fromDate, toDate, 0, 0, 0);
            var item = new ChartViewModel();
            if (list != null && list.Count > 0)
            {
                //sort
                var temps = list.GroupBy(p => new { p.BranchName, p.CreateDate_MMYY }).Select(p => new
                {
                    BranchName = p.Key.BranchName,
                    CreateDate = p.Key.CreateDate_MMYY,
                    PurchaseQuantity = p.Sum(c => c.PurchaseQuantity)

                }).ToList();
                item.ChartModels.AddRange(temps.GroupBy(p => p.BranchName).Select(p => new ChartModel
                {
                    Label = p.Key,
                    Data = p.Select(c => (decimal?)c.PurchaseQuantity).ToArray()
                }));

                item.ChartLabels = temps.Select(p => p.CreateDate).ToArray();
            }

            return this.Ok(item);
        }


        public async Task<List<PurchaseOrderViewModel>> GetPurchaseOrderListMain(string filter, string value, string fromDate, string toDate, int allData, int page, int size)
        {
            var unit = this._unitOfWork.PurchaseOrders;
            var results = unit.GetPurchaseOrderList(
                            fromDate.BuildDateTimeFromSEFormat(),
                            toDate.BuildLastDateTimeFromSEFormat(),
                            value,
                            this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                            page, size, allData > 0
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
                    BranchNameIO = item.BranchNameIO,
                    CreateDate = item.CreatedDate,
                    PurchaseQuantity = item.PurchaseQuantity,
                    IOQuantity = item.IOQuantity,
                    RemainQuantity = item.PurchaseQuantity - item.IOQuantity,
                    Note = item.Note,
                });
            }

            return list;
        }

        [HttpGet("getpurchaseorderdetails")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> GetPurchaseOrderListDetail(string filter, string value, string fromDate, string toDate, int page, int size)
        {
            var unit = this._unitOfWork.PurchaseOrders;
            var results = GetPurchaseOrderListDetailMain(
                            filter,
                            value,
                            fromDate.BuildDateTimeFromSEFormat(),
                            toDate.BuildLastDateTimeFromSEFormat(),
                            page, size
                            );
            return Json(new
            {
                Total = unit.Total,
                List = results
            });
        }

        [HttpGet("reportpurchaseorderdetails")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> ReportPurchaseOrderListDetail(string filter, string value, string fromDate, string toDate, int page, int size)
        {

            var list = GetPurchaseOrderListDetailMain(
                            filter,
                            value,
                            fromDate.BuildDateTimeFromSEFormat(),
                            toDate.BuildLastDateTimeFromSEFormat(),
                            0, 0
                            );
            var item = new ChartViewModel();
            if (list != null && list.Count > 0)
            {
                //sort
                var temps = list.GroupBy(p => new { p.BranchName, p.CreateDate_MMYY }).Select(p => new
                {
                    BranchName = p.Key.BranchName,
                    CreateDate = p.Key.CreateDate_MMYY,
                    PurchaseQuantity = p.Sum(c => c.PurchaseQuantity)

                }).ToList();
                item.ChartModels.AddRange(temps.GroupBy(p => p.BranchName).Select(p => new ChartModel
                {
                    Label = p.Key,
                    Data = p.Select(c => (decimal?)c.PurchaseQuantity).ToArray()
                }));

                item.ChartLabels = temps.Select(p => p.CreateDate).ToArray();
            }

            return this.Ok(item);
        }

        [HttpGet("getpurchasedetailsbyid")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetPurchaseDetails(Guid? index)
        {
            if (index.HasValue)
            {
                var list = GetPurchaseOrderListDetailMain(string.Empty, index.ToString(), ObjectHelper.GetDateMin, ObjectHelper.GetDateNow, 0, 0);
                list = list.Where(item => (item.PurchaseQuantity - item.IOQuantity) > 0).ToList();

                var iodNews = new List<IOStockDetailViewModel>();
                foreach (var item in list)
                {
                    var itemMate = await this._unitOfWork.Materials.Get(item.MaterialId);
                    var itemGrpMate = await this._unitOfWork.GrpMaterials.FindById(itemMate.GrpMaterialId);
                    var itemType = await this._unitOfWork.TypeMaterials.FindById(itemGrpMate.TypeMaterialId);
                    iodNews.Add(new IOStockDetailViewModel
                    {
                        ID = null,
                        PurchaseOrderDetailId = item.PurchaseOrderDetailId,
                        PurchaseOrderId = item.PurchaseOrderId,
                        MaterialId = item.MaterialId,
                        MaterialCode = itemMate.MaterialCode,
                        MaterialName = itemMate.MaterialName,
                        GrpMaterial = itemGrpMate != null ? itemGrpMate.GrpMaterialName : string.Empty,
                        TypeMaterial = itemType != null ? itemType.TypeMaterialName : string.Empty,
                        Quantity = item.PurchaseQuantity - item.IOQuantity,
                        SellPrice = item.SellPrice,
                        TotalPrice = (item.PurchaseQuantity - item.IOQuantity) * item.TotalPrice
                    });

                }
                return Ok(iodNews);
            }
            return BadRequest(ModelState);
        }

        public List<PurchaseOrderViewModel> GetPurchaseOrderListDetailMain(string filter, string value, DateTime fromDate, DateTime toDate, int page, int size)
        {
            var unit = this._unitOfWork.PurchaseOrders;
            var results = unit.GetPurchaseOrderListDetail(
                                fromDate,
                                toDate,
                                value,
                                this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                                string.Empty,
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
                    PurchaseOrderDetailId = item.PurchaseOrderDetailId,
                    PurchaseOrderId = item.PurchaseOrderId,
                    MaterialId = item.MaterialId,
                    MaterialCode = item.MaterialCode,
                    MaterialName = item.MaterialName,
                    PurchaseQuantity = item.PurchaseQuantity,
                    IOQuantity = item.IOQuantity,
                    SellPrice = item.SellPrice,
                    RemainQuantity = item.PurchaseQuantity - item.IOQuantity,
                    Note = item.Note,
                });
            }

            return list;
        }

        [HttpGet("getpurchaseorderdetailhistorys")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> GetPurchaseOrderDetailListHistory(string filter, string value, string fromDate, string toDate, int page, int size)
        {
            var unit = this._unitOfWork.PurchaseOrders;
            var results = unit.GetPurchaseOrderListDetailHistory(
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
                    ID = item.IOStockId,
                    Code = item.IONumber,
                    FullName = item.FullName,
                    CreateDate = item.CreatedDate,
                    InputQuantity = item.InputQuantity,
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

