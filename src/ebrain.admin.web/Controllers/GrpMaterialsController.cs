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
    [Security("D7239078-E67A-42FA-86D6-4A8C3F73D721", "D7239078-E67A-42FA-86D6-4A8C3F73D735"
        , "8AA6E971-1C3D-4835-B154-D662CE12AE91")]
    public class GrpMaterialsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public GrpMaterialsController(IUnitOfWork unitOfWork, ILogger<GrpMaterialsController> logger) : base(unitOfWork, logger)
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
            var unit = this._unitOfWork.GrpMaterials;
            var ret = from c in await unit.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId), false, null, page, size)
                      select new GrpMaterialViewModel
                      {
                          ID = c.GrpMaterialId,
                          Code = c.GrpMaterialCode,
                          Name = c.GrpMaterialName,
                          Note = c.Note,
                          TypeMaterialName = this._unitOfWork.TypeMaterials.FindNameById(c.TypeMaterialId)
                      };

            return Json(new
            {
                Total = unit.Total,
                List = ret
            });
        }

        [HttpGet("findbytypeid")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<GrpMaterialViewModel>> FindByTypeId(string typeid)
        {
            var ret = from c in await this._unitOfWork.GrpMaterials.FindByTypeId(typeid, this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new GrpMaterialViewModel
                      {
                          ID = c.GrpMaterialId,
                          Code = c.GrpMaterialCode,
                          Name = c.GrpMaterialName,
                          Note = c.Note,
                          TypeMaterialId = c.TypeMaterialId
                      };

            return ret;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] GrpMaterialViewModel value)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.GrpMaterials.Save(new GrpMaterial
                {
                    GrpMaterialId = Guid.NewGuid(),
                    GrpMaterialCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    GrpMaterialName = value.Name,
                    Note = value.Note,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    TypeMaterialId = value.TypeMaterialId,
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
                var ret = await this._unitOfWork.GrpMaterials.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<GrpMaterialViewModel> Get(Guid? index)
        {
            var c = await this._unitOfWork.GrpMaterials.FindById(index);

            return new GrpMaterialViewModel
            {
                TypeMaterialId = c.TypeMaterialId,
                ID = c.GrpMaterialId,
                Code = c.GrpMaterialCode,
                Name = c.GrpMaterialName,
                Note = c.Note,
            };
        }
    }
}
