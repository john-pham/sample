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
    public class SuppliersController : Controller
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public SuppliersController(IUnitOfWork unitOfWork, ILogger<SuppliersController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> Search(string filter, string value, int isOption)
        {
            var userId = new Guid(Utilities.GetUserId(this.User));
            var ret = from c in await this._unitOfWork.Suppliers.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId), isOption)
                      select new SupplierViewModel
                      {
                          ID = c.SupplierId,
                          Code = c.SupplierCode,
                          Name = c.SupplierName,
                          TaxCode = c.TaxCode,
                          Address = c.Address,
                          AccountBank = c.AccountBank,
                          Phone = c.Phone,
                          Email = c.Email,
                          Fax = c.Fax,
                          Note = c.Note,
                          GrpSupplierId = c.GrpSupplierId
                      };

            return Ok(ret);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SupplierViewModel value)
        {
            if (ModelState.IsValid)
            {
                var userId = new Guid(Utilities.GetUserId(this.User));
                var ret = await this._unitOfWork.Suppliers.Save(new Supplier
                {
                    SupplierId = Guid.NewGuid(),
                    SupplierCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    SupplierName = value.Name,
                    Note = value.Note,
                    TaxCode = value.TaxCode,
                    Address = value.Address,
                    AccountBank = value.AccountBank,
                    Phone = value.Phone,
                    Email = value.Email,
                    Fax = value.Fax,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Birthday = value.Birthday,
                    GrpSupplierId = value.GrpSupplierId
                }, value.ID);

                return Ok(ret);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<SupplierViewModel> Get(Guid? index)
        {
            var c = await this._unitOfWork.Suppliers.Get(index);
            return new SupplierViewModel
            {
                ID = c.SupplierId,
                Code = c.SupplierCode,
                Name = c.SupplierName,
                TaxCode = c.TaxCode,
                Address = c.Address,
                AccountBank = c.AccountBank,
                Phone = c.Phone,
                Email = c.Email,
                Fax = c.Fax,
                Note = c.Note,
                Birthday = c.Birthday,
                GrpSupplierId = c.GrpSupplierId
            };
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] String id)
        {
            if (ModelState.IsValid)
            {
                var userId = new Guid(Utilities.GetUserId(this.User));
                var ret = await this._unitOfWork.Suppliers.Delete(id);
                return Ok(ret);
            }
            return BadRequest(ModelState);
        }
    }
}
