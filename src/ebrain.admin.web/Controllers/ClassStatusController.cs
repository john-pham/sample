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
    public class ClassStatusController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public ClassStatusController(IUnitOfWork unitOfWork, ILogger<ClassStatusController> logger) : base(unitOfWork, logger)
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

        [HttpGet("getall")]
        [Produces(typeof(UserViewModel))]
        public async Task<IEnumerable<ClassStatusViewModel>> GetAll(string index)
        {
            var ret = from c in await this._unitOfWork.ClassStatus.GetAll(this._unitOfWork.Branches.GetAllBranchOfUserString(userId))
                      select new ClassStatusViewModel
                      {
                          ID = c.ClassStatusId,
                          Code = c.ClassStatusCode,
                          Name = c.ClassStatusName,
                          Note = c.Note
                      };

            return ret;
        }
        

    }
}
