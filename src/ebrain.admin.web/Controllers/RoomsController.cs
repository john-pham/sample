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
    [Security("D7239078-E67A-42FA-86D6-4A8C3F73D533")]
    public class RoomsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public RoomsController(IUnitOfWork unitOfWork, ILogger<RoomsController> logger) : base(unitOfWork, logger)
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
        public async Task<JsonResult> Search(string filter, string value, int page, int size)
        {
            var unit = this._unitOfWork.Rooms;
            var ret = from c in await unit.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId), page, size)
                      select new RoomViewModel
                      {
                          ID = c.RoomId,
                          Code = c.RoomCode,
                          Name = c.RoomName,
                          Note = c.Note
                      };

            return Json(new
            {
                Total = unit.Total,
                List = ret
            });
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] RoomViewModel value)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.Rooms.Save(new Room
                {
                    RoomId = Guid.NewGuid(),
                    RoomCode = value.Code,
                    RoomName = value.Name,
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
                var ret = await this._unitOfWork.Rooms.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<RoomViewModel> Get(Guid? index)
        {
            var c = await this._unitOfWork.Rooms.Get(index);
            return new RoomViewModel
            {
                ID = c.RoomId,
                Code = c.RoomCode,
                Name = c.RoomName,
                Note = c.Note
            };
        }
    }
}
