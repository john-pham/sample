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
    [Security("D7239079-E67A-42FA-86D6-4A8C3F73D534")]
    public class StocksController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public StocksController(IUnitOfWork unitOfWork, ILogger<StocksController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<StockViewModel>> Search(string filter, string value)
        {
            var ret = from c in await this._unitOfWork.Stocks.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new StockViewModel
                      {
                          ID = c.StockId,
                          Code = c.StockCode,
                          Name = c.StockName,
                          Note = c.Note
                      };

            return ret;
        }

        private Guid userId
        {
            get
            {
                return Utilities.GetUserId(this.User);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] StockViewModel value)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.Stocks.Save(new Stock
                {
                    StockId = Guid.NewGuid(),
                    StockCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    StockName = value.Name,
                    Note = value.Note,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
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
                var ret = await this._unitOfWork.Stocks.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }
    }
}
