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

namespace Ebrain.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Security("")]
    public class GrpSuppliersController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public GrpSuppliersController(IUnitOfWork unitOfWork, ILogger<GrpSuppliersController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<GrpSupplierViewModel>> Search(string filter, string value)
        {
            var userId = Utilities.GetUserId(this.User);
            var ret = from c in await this._unitOfWork.GrpSuppliers.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new GrpSupplierViewModel
                      {
                          ID = c.GrpSupplierId,
                          Code = c.GrpSupplierCode,
                          Name = c.GrpSupplierName,
                          Note = c.Note,
                          IsCustomer = c.IsCustomer,
                          IsEmployee = c.IsEmployee,
                          IsSupplier = c.IsSupplier,
                          IsTeacher = c.IsTeacher
                      };

            return ret;
        }

        [HttpGet("getall")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<GrpSupplierViewModel>> GetAll(int option)
        {
            var userId = Utilities.GetUserId(this.User);
            var ret = from c in await this._unitOfWork.GrpSuppliers.GetAll(this._unitOfWork.Branches.GetAllBranchOfUserString(userId), option)
                      select new GrpSupplierViewModel
                      {
                          ID = c.GrpSupplierId,
                          Code = c.GrpSupplierCode,
                          Name = c.GrpSupplierName,
                          Note = c.Note,
                          IsCustomer = c.IsCustomer,
                          IsEmployee = c.IsEmployee,
                          IsSupplier = c.IsSupplier,
                          IsTeacher = c.IsTeacher
                      };

            return ret;
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<GrpSupplierViewModel> Get(Guid? index)
        {
            var c = await this._unitOfWork.GrpSuppliers.Get(index);

            return new GrpSupplierViewModel
            {
                ID = c.GrpSupplierId,
                Code = c.GrpSupplierCode,
                Name = c.GrpSupplierName,
                Note = c.Note,
                IsCustomer = c.IsCustomer,
                IsEmployee = c.IsEmployee,
                IsSupplier = c.IsSupplier,
                IsTeacher = c.IsTeacher
            };
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] GrpSupplierViewModel value)
        {
            if (ModelState.IsValid)
            {
                var userId = Utilities.GetUserId(this.User);

                var ret = await this._unitOfWork.GrpSuppliers.Save(new GrpSupplier
                {
                    GrpSupplierId = Guid.NewGuid(),
                    GrpSupplierCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    GrpSupplierName = value.Name,
                    Note = value.Note,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsCustomer = value.IsCustomer,
                    IsEmployee = value.IsEmployee,
                    IsSupplier = value.IsSupplier,
                    IsTeacher = value.IsTeacher
                }, value.ID);

                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] String id)
        {
            if (ModelState.IsValid)
            {
                var userId = Utilities.GetUserId(this.User);

                var ret = await this._unitOfWork.GrpSuppliers.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }
    }
}
