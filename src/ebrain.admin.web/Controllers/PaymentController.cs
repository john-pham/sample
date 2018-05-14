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
    [Security("376F3FFE-408A-49A8-B0EA-B69654E11B31", "376F3FFE-408A-49A8-B0EA-B69654E11B32", "376F3FFE-408A-49A8-B0EA-B69654E11B33",
        "376F3FFE-408A-49A8-B0EA-B69654E11B34")]
    public class PaymentController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        private readonly IAccountManager _accountManager;

        public PaymentController(IUnitOfWork unitOfWork, ILogger<PaymentController> logger) : base(unitOfWork, logger)
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

        [HttpPost("{id}")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<PaymentTypeViewModel>> DetailReceipt(string id)
        {
            return await GetPaymentType(false);
        }

        [HttpGet("getpaymenttype")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<PaymentTypeViewModel>> GetPaymentTypeReciept(string filter, string value)
        {
            return await GetPaymentType(false);
        }

        [HttpGet("getpaymenttypevoucher")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<PaymentTypeViewModel>> GetPaymentTypeVoucher(string filter, string value)
        {
            return await GetPaymentType(true);
        }

        private async Task<IEnumerable<PaymentTypeViewModel>> GetPaymentType(bool isPayment)
        {
            var results = await this._unitOfWork.Payments.GetAllPaymentTypes(isPayment, this._unitOfWork.Branches.GetAllBranchOfUserString(userId));
            var list = new List<PaymentTypeViewModel>();
            foreach (var item in results)
            {
                list.Add(new PaymentTypeViewModel
                {
                    ID = item.PaymentTypeId,
                    Code = item.PaymentTypeCode,
                    Name = item.PaymentTypeName,
                });
            }
            return list;
        }

        [HttpGet("reportpaymentsummarize")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> ReportPaymentSummarize(string filter, string value, string fromDate, string toDate, int page, int size)
        {
            var list = await GetPaymentSummarizeMain(filter, value, fromDate, toDate, 0, 0);
            var item = new ChartViewModel();
            if (list != null && list.Count > 0)
            {
                //sort
                list = list.OrderBy(p => p.CreateDate).ToList();
                item.ChartModels.AddRange(list.GroupBy(p => p.BranchName).Select(p => new ChartModel
                {
                    Label = p.Key,
                    Data = p.Select(c => c.TotalPrice).ToArray()
                }));

                item.ChartLabels = list.Select(p => p.CreateDate.ToString("dd/MM")).ToArray();
            }

            return this.Ok(item);

        }

        [HttpGet("searchpaymentsummarize")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> SearchPaymentSummarize(string filter, string value, string fromDate, string toDate, int page, int size)
        {
            var list = await GetPaymentSummarizeMain(filter, value, fromDate, toDate, page, size);
            return Json(new
            {
                Total = this._unitOfWork.Payments.Total,
                List = list
            });
        }

        private async Task<List<PaymentViewModel>> GetPaymentSummarizeMain(string filter, string value, string fromDate, string toDate, int page, int size)
        {
            //get userId accessRightPerson
            var userAccessRightPerson = await this._unitOfWork.AccessRightPersons.GetUserIdFromAccessRightPerson(Guid.Parse(Constants.PAYMENTLIST), userId);
            var unit = this._unitOfWork.Payments;
            var results = unit.GetPaymentList(
                fromDate.BuildDateTimeFromSEFormat(),
                toDate.BuildLastDateTimeFromSEFormat(),
                value,
                0, false,
                userAccessRightPerson,
                this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                page, size);

            var list = new List<PaymentViewModel>();
            foreach (var item in results)
            {
                list.Add(new PaymentViewModel
                {
                    ID = item.PaymentId,
                    Code = item.PaymentCode,
                    CreateDate = item.CreatedDate,
                    TotalPrice = item.TotalPrice,
                    PaymentTypeId = (int)item.PaymentTypeId,
                    PaymentTypeName = item.PaymentTypeName,
                    FullName = item.FullName,
                    Note = item.Note,
                    BranchName = item.BranchName
                });
            }

            return list;
        }

        [HttpGet("searchpaymentdetail")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> SearchPaymentDetail(string filter, string value, string fromDate, string toDate, int page, int size)
        {
            //get userId accessRightPerson
            var userAccessRightPerson = await this._unitOfWork.AccessRightPersons.GetUserIdFromAccessRightPerson(Guid.Parse(Constants.PAYMENTDETAIL), userId);
            var unit = this._unitOfWork.Payments;
            var results = unit.GetPaymentDetailList(
                    fromDate.BuildDateTimeFromSEFormat(),
                    toDate.BuildLastDateTimeFromSEFormat(),
                    value,
                    0, false, userAccessRightPerson, this._unitOfWork.Branches.GetAllBranchOfUserString(userId),
                    page, size);
            var list = new List<PaymentViewModel>();
            foreach (var item in results)
            {
                list.Add(new PaymentViewModel
                {
                    ID = item.PaymentId,
                    Code = item.PaymentCode,
                    CreateDate = item.CreatedDate,
                    TotalPrice = item.TotalPrice,
                    PaymentTypeId = (int)item.PaymentTypeId,
                    PaymentTypeName = item.PaymentTypeName,
                    FullName = item.FullName,
                    Note = item.Note,
                    IOStockId = item.IOStockId,
                    IONumber = item.IONumber
                });
            }

            return Json(new
            {
                Total = unit.Total,
                List = list
            });
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<PaymentViewModel>> Search(string filter, string value)
        {
            var results = await this._unitOfWork.Payments.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId));
            var list = new List<PaymentViewModel>();
            foreach (var item in results)
            {
                list.Add(new PaymentViewModel
                {
                    ID = item.PaymentId,
                    Code = item.PaymentCode,
                    CreateBy = item.CreatedBy,
                    CreateDate = item.CreatedDate,
                    TotalMoney = item.TotalMoney,
                    PaymentTypeId = (int)item.PaymentTypeId,
                    Note = item.Note,

                });
            }
            return list;
        }


        [HttpGet("getdefault")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetDefaultAsync(Guid? index)
        {
            if (index.IsNullOrDefault())
            {
                var ioNumber = this._unitOfWork.ConfigNumberOfCodes.GenerateCodePayment((int)EnumPayment.PaymentIOOUT, userId.ToString());

                var itemNew = new PaymentViewModel
                {
                    Code = ioNumber,
                    CreateDate = DateTime.Now,
                    CreateBy = userId,
                    PaymentTypeId = (int)EnumPayment.PaymentIOOUT
                };

                itemNew.IODetails = new PaymentDetailViewModel[0];
                return Ok(itemNew);
            }
            else return await GetPayment(index);
        }

        private async Task<IActionResult> GetPayment(Guid? id)
        {
            var ret = await this._unitOfWork.Payments.FindById(id);
            if (ret != null)
            {
                var iods = await this._unitOfWork.Payments.GetDetailByIOId(ret.PaymentId);
                var iodNews = new List<PaymentDetailViewModel>();
                foreach (var item in iods)
                {
                    iodNews.Add(new PaymentDetailViewModel
                    {
                        ID = item.PaymentDetailId,
                        IOStockId = item.IOStockId,
                        Code = item.IONumber,
                        TotalPrice = item.TotalPrice,
                        TotalPriceExist = item.TotalPriceExist,
                        TotalPricePayment = item.TotalPricePayment,
                        Note = item.Note
                    });
                }

                return Ok(new PaymentViewModel
                {
                    ID = ret.PaymentId,
                    Code = ret.PaymentCode,
                    PaymentTypeName = ret.PaymentName,
                    IONumber = ret.PaymentCode,
                    BranchId = ret.BranchId,
                    CreateBy = ret.CreatedBy,
                    CreateDate = ret.CreatedDate,
                    PaymentTypeId = ret.PaymentTypeId,
                    Note = ret.Note,
                    IODetails = iodNews.ToArray()
                });
            }
            return BadRequest(ModelState);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] PaymentViewModel value)
        {
            if (ModelState.IsValid)
            {
                var ioId = Guid.NewGuid();
                var io = new Payment
                {
                    PaymentId = ioId,
                    PaymentCode = value.Code,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = userId,
                    UpdatedDate = DateTime.Now,
                    PaymentTypeId = value.PaymentTypeId,
                    BranchId = ioId,
                    Note = value.Note
                };
                var ioDetails = value.IODetails.Select(p => new PaymentDetail
                {
                    PaymentDetailId = p.ID == null ? Guid.NewGuid() : p.ID.Value,
                    PaymentId = ioId,
                    IOStockId = p.IOStockId,
                    IONumber = p.Code,
                    PriceBeforeVAT = 0,
                    PriceAfterVAT = 0,
                    VAT = 0,
                    TotalPricePayment = p.TotalPricePayment,
                    TotalPriceExist = p.TotalPriceExist,
                    TotalPrice = p.TotalPrice,
                    Note = p.Note,

                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = userId,
                    UpdatedDate = DateTime.Now

                });

                io.TotalMoney = ioDetails.Sum(p => p.TotalPricePayment);
                io.TotalMoneyAgain = ioDetails.Sum(p => p.TotalPrice - p.TotalPricePayment);
                io.TotalMoneyReturn = ioDetails.Sum(p => p.TotalPrice - p.TotalPricePayment);

                var ret = await this._unitOfWork.Payments.Save(io, ioDetails.ToArray(), value.ID);

                return await GetPayment(ret.PaymentId);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("deletemaster")]
        public async Task<IActionResult> DeleteMaster([FromBody] Guid? id)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.Payments.DeleteMaster(id);
                return await GetDefaultAsync(null);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelMaster([FromBody] Guid? id)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.Payments.CancelMaster(id);
                return await GetDefaultAsync(null);
            }

            return BadRequest(ModelState);
        }

    }
}
