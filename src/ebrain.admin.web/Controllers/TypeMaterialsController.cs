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
    [Security("D7239078-E67A-42FA-86D6-4A8C3F73D571", "8AA6E971-1C3D-4835-B154-D662CE12AE9E")]
    public class TypeMaterialsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public TypeMaterialsController(IUnitOfWork unitOfWork, ILogger<TypeMaterialsController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<TypeMaterialViewModel>> Search(string filter, string value)
        {
            var ret = from c in await this._unitOfWork.TypeMaterials.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new TypeMaterialViewModel
                      {
                          ID = c.TypeMaterialId,
                          Code = c.TypeMaterialCode,
                          Name = c.TypeMaterialName,
                          Note = c.Note,
                          IsDocument = c.IsDocument,
                          IsLearning = c.IsLearning
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

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<TypeMaterialViewModel> Get(Guid? index)
        {
            var item = await this._unitOfWork.TypeMaterials.FindById(index);

            return new TypeMaterialViewModel
            {
                ID = item.TypeMaterialId,
                Code = item.TypeMaterialCode,
                Name = item.TypeMaterialName,
                Note = item.Note,
                IsDocument = item.IsDocument,
                IsLearning = item.IsLearning
            }; ;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] TypeMaterialViewModel value)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.TypeMaterials.Save(new TypeMaterial
                {
                    TypeMaterialId = Guid.NewGuid(),
                    TypeMaterialCode = value.Code,
                    BranchId = Guid.NewGuid(),
                    TypeMaterialName = value.Name,
                    Note = value.Note,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDocument = value.IsDocument,
                    IsLearning = value.IsLearning
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
                var ret = await this._unitOfWork.TypeMaterials.Delete(id);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }
    }
}
