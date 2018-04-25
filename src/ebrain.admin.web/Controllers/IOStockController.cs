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
    [Security("9ECBD467-7642-467B-AAE2-96484AD182A1", "9ECBD467-7642-467B-AAE2-96484AD182A2",
        "9ECBD467-7642-467B-AAE2-96484AD182A3", "9ECBD467-7642-467B-AAE2-96484AD182A4", "9ECBD467-7642-467B-AAE2-96484AD182A5",
        "9ECBD467-7642-467B-AAE2-96484AD182A5", "9ECBD467-7642-467B-AAE2-96484AD182A8", "9ECBD467-7642-467B-AAE2-96484AD182A9")]
    public class IOStockController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;

        public IOStockController(IUnitOfWork unitOfWork, ILogger<IOStockController> logger) : base(unitOfWork, logger)
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

        [HttpGet("getiobyiotypeid")]
        [Produces(typeof(UserViewModel))]
        public IEnumerable<IOStockViewModel> GetIOByIOTypeId(string filter, string value, string fromDate, string toDate)
        {
            return GetIOByIOTypeIdMain(filter, value, fromDate.BuildDateTimeFromSEFormat(), toDate.BuildLastDateTimeFromSEFormat(), 0);

        }

        public IEnumerable<IOStockViewModel> GetIOByIOTypeIdMain(string filter, string value, DateTime fromDate, DateTime toDate, int ioTypeId)
        {
            // var results = await this._unitOfWork.IOStocks.Search(filter, value);
            var results = this._unitOfWork.IOStocks.GetIOStockList(
                            fromDate,
                            toDate,
                            value, ioTypeId, this._unitOfWork.Branches.GetAllBranchOfUserString(userId));
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
                    StudentId = item.StudentId
                });
            }
            return list;
        }

        [HttpGet("getionew")]
        [Produces(typeof(UserViewModel))]
        public IActionResult GetIONew()
        {
            var dt = DateTime.Now;
            var list = GetIOByIOTypeIdMain(string.Empty, string.Empty, dt.Date, new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59), (int)EnumIOType.IORegisCourse);
            return this.Ok(list.Count());

        }

        [HttpGet("getioall")]
        [Produces(typeof(UserViewModel))]
        public IActionResult GetIOAll()
        {
            var dt = DateTime.Now;
            var list = GetIOByIOTypeIdMain(string.Empty, string.Empty, new DateTime(1900, 01, 01), dt, (int)EnumIOType.IORegisCourse);
            return this.Ok(list.Count());

        }

        [HttpGet("getiodetailbyiotypeid")]
        [Produces(typeof(UserViewModel))]
        public IEnumerable<IOStockViewModel> GetIODetailByIOTypeId(string filter, string value, string fromDate, string toDate)
        {
            var results = this._unitOfWork.IOStocks.GetIOStockDetailList(
                            fromDate.BuildDateTimeFromSEFormat(),
                            toDate.BuildLastDateTimeFromSEFormat(),
                            value,
                            0,
                            this._unitOfWork.Branches.GetAllBranchOfUserString(userId)
                        );
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
                    IOStockDetailId = item.IOStockDetailId,
                    MaterialId = item.MaterialId,
                    MaterialCode = item.MaterialCode,
                    MaterialName = item.MaterialName,
                    Quantity = item.InputQuantity
                });
            }
            return list;
        }

        [HttpGet("getwarehousecard")]
        [Produces(typeof(UserViewModel))]
        public IEnumerable<IOStockViewModel> GetWarehouseCard(string filter, string value, string fromDate, string toDate)
        {
            var results = this._unitOfWork.IOStocks.GetWarehouseCard(
                            fromDate.BuildDateTimeFromSEFormat(),
                            toDate.BuildLastDateTimeFromSEFormat(),
                            value,
                            0,
                            this._unitOfWork.Branches.GetAllBranchOfUserString(userId)
                        );
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
                    IOStockDetailId = item.IOStockDetailId,
                    MaterialId = item.MaterialId,
                    MaterialCode = item.MaterialCode,
                    MaterialName = item.MaterialName,
                    Quantity = item.InputQuantity,
                    QuantityInput = item.QuantityInput,
                    QuantityOutput = item.QuantityOutput
                });
            }
            return list;
        }

        [HttpGet("getiopayment")]
        [Produces(typeof(UserViewModel))]
        public Task<IEnumerable<IOStockViewModel>> GetIOPaymentReceipt(string filter, string value, string ioId, string fromDate, string toDate)
        {
            return GetIOPayment(filter, value, false, ioId, fromDate, toDate);
        }

        [HttpGet("getiopaymentvoucher")]
        [Produces(typeof(UserViewModel))]
        public Task<IEnumerable<IOStockViewModel>> GetIOPaymentVoucher(string filter, string value, string ioId, string fromDate, string toDate)
        {
            return GetIOPayment(filter, value, true, ioId, fromDate, toDate);
        }

        public async Task<IEnumerable<IOStockViewModel>> GetIOPayment(string filter, string value, bool isInput, string ioId, string fromDate, string toDate)
        {
            var frDate = fromDate.BuildDateTimeFromSEFormat();
            var tDate = toDate.BuildLastDateTimeFromSEFormat();
            if (!string.IsNullOrEmpty(ioId))
            {
                var io = await this._unitOfWork.IOStocks.FindById(Guid.Parse(ioId));
                if (io != null)
                {
                    frDate = new DateTime(io.CreatedDate.Year, io.CreatedDate.Month, io.CreatedDate.Day);
                }
            }
            var results = this._unitOfWork.IOStocks
                 .GetIOStockPaymentList(
                    frDate,
                    tDate,
                    value,
                    ioId,
                    0, isInput
                 , this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                 .Where(p => p.TotalPriceExist > 0);

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
                    TotalPriceExist = item.TotalPriceExist,
                    TotalPricePayment = item.TotalPricePayment
                });
            }
            return list;
        }


    }
}
