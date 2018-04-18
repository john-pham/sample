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
    public class PaymentTypesController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public PaymentTypesController(IUnitOfWork unitOfWork, ILogger<PaymentTypesController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<PaymentTypeViewModel>> Search(string filter, string value)
        {
            var ret = from c in await this._unitOfWork.PaymentTypes.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new PaymentTypeViewModel
                      {
                          ID = 1,
                          Code = c.PaymentTypeCode,
                          Name = c.PaymentTypeName,
                          Note = c.Note
                      };

            return ret;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] PaymentTypeViewModel value)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.PaymentTypes.Save(new PaymentType
                {
                    PaymentTypeId = 1,
                    PaymentTypeCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    PaymentTypeName = value.Name,
                    Note = value.Note,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                });

                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] String id)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.PaymentTypes.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        private Guid userId
        {
            get
            {
                return Utilities.GetUserId(this.User);
            }
        }
    }
}
